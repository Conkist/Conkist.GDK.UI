using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace Conkist.UI
{
    public class DialogBoxBehaviour : MonoBehaviour
    {
        [SerializeField] TMP_Text _tmpTitle;
        [SerializeField] TMP_Text _tmpMessage;
        [Header("Options")]
        [SerializeField] Transform _optionParent;
        [SerializeField] DialogOptionHandler _outputOptionPrefab;

        public UnityEvent OnPopUp;
        public UnityEvent OnPopOut;

        public string Title { get => _tmpTitle.text; set { _tmpTitle.SetText(value); } }
        public string Message { get => _tmpMessage.text; set { _tmpMessage.SetText(value); } }

        public void ClearOptions()
        {
            foreach (Transform t in _optionParent) Destroy(t.gameObject);
        }

        public DialogBoxBehaviour AddOption()
        {
            AddOption("OK", () => AnimatePopOut());
            return this;
        }

        public DialogBoxBehaviour AddOption(string text, UnityAction onClick)
        {
            var op = Instantiate(_outputOptionPrefab, _optionParent).GetComponent<DialogOptionHandler>();
            op.OptionText = text;
            op.OptionAction = onClick;
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
            AnimatePopOut();
        }

        private void AnimatePopOut(Action callback = null)
        {
            //Make Logic for popout feedback before it destroys the scene
            callback?.Invoke();
            OnPopOut?.Invoke();
        }
    }
}