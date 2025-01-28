using Conkist.GDK;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Conkist.UI
{
    public class DialogOption
    {
        public string Text;
        public UnityAction OnClick;

        public DialogOption(string text)
        {
            Text = text;
        }

        public void AddLoadSceneClick(string sceneAddress)
        {
            OnClick += () => {
                SceneManager.LoadSceneAsync(sceneAddress, loadType: LoadType.Hidden).Forget();
            };
        }

        public void AddReloadAppClick(){
            OnClick += () => {
                SceneManager.ReloadApplication().Forget();
            };
        }

        public void AddExternalLinkClick(string url)
        {
            OnClick += () => {
                Application.OpenURL(url);
            };
        }
    }
}