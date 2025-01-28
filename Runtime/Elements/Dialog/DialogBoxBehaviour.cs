using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Conkist.GDK;
using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Conkist.UI
{
    public class DialogBoxBehaviour : MonoBehaviour
    {
        [SerializeField] TMP_Text _tmpTitle;
        [SerializeField] TMP_Text _tmpMessage;

        [Header("Options")]
        public DialogOption[] Options;
        
        [SerializeField] Transform _optionParent;
        [SerializeField] DialogOptionHandler _outputOptionPrefab;

        public UnityEvent OnPopUp;
        public UnityEvent OnPopOut;

        public string Title { get => _tmpTitle.text; set { _tmpTitle.SetText(value); } }
        public string Message { get => _tmpMessage.text; set { _tmpMessage.SetText(value); } }

        private SceneInstance _sceneInstance;

        public static async UniTask<DialogBoxBehaviour> PopUp(string popupTypeAddress, string title, string message, DialogOption[] options = null)
        {
            var instance = await SceneManager.AddSceneAsync(popupTypeAddress);
            var dialog = FindFirstObjectByType(typeof(DialogBoxBehaviour)) as DialogBoxBehaviour;
            dialog._sceneInstance = instance;
            dialog.Title = title;
            dialog.Message = message;

            if (options != null)
            {
                dialog.ClearOptions();
                foreach (var option in options)
                {
                    dialog.AddOption(option.Text, option.OnClick);
                }
            }else
            {
                dialog.ClearOptions();
                dialog.AddOption();
            }

            dialog.OnPopUp?.Invoke();
            return dialog;
        }

        public static DialogBoxBehaviour GetResult(UniTask<DialogBoxBehaviour> dialogTask)
        {
            return dialogTask.GetAwaiter().GetResult();
        }

        public void ClearOptions()
        {
            foreach (Transform t in _optionParent) Destroy(t.gameObject);
        }

        public DialogBoxBehaviour AddOption()
        {
            AddOption("OK", () => PopOut());
            return this;
        }

        public DialogBoxBehaviour AddOption(string text, UnityAction onClick)
        {
            var op = Instantiate(_outputOptionPrefab, _optionParent).GetComponent<DialogOptionHandler>();
            op.OptionText = text;
            op.OptionAction = onClick;
            return this;
        }

        public DialogBoxBehaviour AddOption(DialogOption option)
        {
            AddOption(option.Text, option.OnClick);
            return this;
        }

        public void TimeOut(int secs, UnityAction onTimeout = null)
        {
            StartCoroutine(DelayedPopOut(secs, onTimeout));
        }

        private IEnumerator DelayedPopOut(int secs, UnityAction onTimeout = null)
        {
            yield return new WaitForSeconds(secs);
            onTimeout?.Invoke();
            PopOut();
        }

        private void PopOut(Action callback = null)
        {
            //Make Logic for popout feedback before it destroys the scene
            callback?.Invoke();
            OnPopOut?.Invoke();
            SceneManager.RemoveScene(_sceneInstance).Forget();
        }
    }
}