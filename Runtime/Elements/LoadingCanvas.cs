using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Conkist.GDK.Loading;
using Cysharp.Threading.Tasks;

namespace Conkist.GDK.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class LoadingCanvas : LoadEventListener
    {
        [Header("GameObjects")]
        /// the text object where you want the loading message to be displayed
        public TextMeshProUGUI LoadingText;
        /// the canvas group containing the progress bar
        public CanvasGroup LoadingProgressBar;
        /// the canvas group containing the animation
        public CanvasGroup LoadingAnimation;
        /// the canvas group containing the animation to play when loading is complete
        public CanvasGroup LoadingCompleteAnimation;

        [Header("Time")]
        /// the duration (in seconds) of the initial fade in
        public float StartFadeDuration = 0.2f;
        /// the speed of the progress bar
        public float ProgressBarSpeed = 2f;
        /// the duration (in seconds) of the load complete fade out
        public float ExitFadeDuration = 0.2f;
        /// the delay (in seconds) before leaving the scene when complete
        public float LoadCompleteDelay = 0.5f;

        protected float _fadeDuration = 0.5f;
        protected float _fillTarget = 0f;
        protected string _loadingTextValue;
        protected Image _progressBarImage;


        public override void DownloadProgress(AssetsDownloadStatus status)
        {
            //TODO Update load status
            base.DownloadProgress(status);
        }

        public override void LoadProgress(float progress)
        {
            _fillTarget = progress;
        }

        protected virtual void Update()
        {
            _progressBarImage.fillAmount = _fillTarget;
        }

        public override UniTask GetLoadingTask(string name, LoadStatus status)
        {
            //TODO add fade events. start and latecomplete already set
            return base.GetLoadingTask(name, status);
        }

        protected override async UniTask OnLoadStarted(string address)
        {
            LoadingCompleteAnimation.alpha = 0;
            _progressBarImage.fillAmount = 0f;
            LoadingText.text = _loadingTextValue;
        }

        protected override async UniTask OnLateLoadCompleted(string address)
        {
            LoadingCompleteAnimation.gameObject.SetActive(true);
            LoadingProgressBar.alpha = 0;
            LoadingAnimation.alpha = 0;
            LoadingCompleteAnimation.alpha = 1;
            //StartCoroutine(MMFade.FadeCanvasGroup(LoadingProgressBar, 0.1f, 0f));
            //StartCoroutine(MMFade.FadeCanvasGroup(LoadingAnimation, 0.1f, 0f));
            //StartCoroutine(MMFade.FadeCanvasGroup(LoadingCompleteAnimation, 0.1f, 1f));

            await UniTask.WaitForSeconds(LoadCompleteDelay);

            await GetLoadingTask(address, LoadStatus.ExitFade);
            await UniTask.WaitForSeconds(ExitFadeDuration);

            await GetLoadingTask(address, LoadStatus.LoadTransitionComplete);
        }
    }
}
