using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;
using Conkist.GDK.Loading;

namespace Conkist.GDK.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class LoadingCanvas : LoadEventListener
    {
        private CanvasGroup _canvasGroup;
        public LoadType loadCanvasType;

        [SerializeField] float _dummyTime = 3f;
        private float dummyProgress;

        [Header("Events")]
        public UnityEvent OnLoadingStarted;
        public UnityEvent<float> OnLoadingProgressUpdate;
        public UnityEvent OnLoadingCompleted;
        public UnityEvent OnLoadingFailed;

        void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public override void DownloadProgress(AssetsDownloadStatus status)
        {
            base.DownloadProgress(status);
        }

        public override void LoadProgress(float progress)
        {
            var visibleProgress = Mathf.Min(progress, dummyProgress);
            OnLoadingProgressUpdate?.Invoke(visibleProgress);
            if(visibleProgress >= 1f)
            EventManager.TriggerEvent(new LoadingEvents.VisibleLoadCompletedEvent());
        }

        protected override async UniTask OnLoadStarted(string address)
        {
            if(loadType == loadCanvasType)
            {
                Debug.Log("Loading Canvas Show");
                _canvasGroup.blocksRaycasts = true;
                _canvasGroup.alpha = 1f;
                OnLoadingStarted?.Invoke();
                DummyLoad().Forget();
            }
        }

        protected override async UniTask OnFadeOut(string address)
        {
            if(loadType == loadCanvasType)
            {
                Debug.Log("Loading Canvas Hide");
                _canvasGroup.blocksRaycasts = false;
                _canvasGroup.alpha = 0f;
                OnLoadingCompleted?.Invoke();
            }
        }

        private async UniTask DummyLoad()
        {
            dummyProgress = 0;
            while(dummyProgress < 1f)
            {
                dummyProgress += Time.deltaTime / _dummyTime;
                OnLoadingProgressUpdate?.Invoke(dummyProgress);
                await UniTask.Yield();
            }
        }
    }
}
