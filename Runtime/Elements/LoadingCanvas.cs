using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;
using Conkist.GDK.Loading;

namespace Conkist.GDK.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class LoadingCanvas : LoadEventListener, 
        EventListener<LoadingEvents.LoadingCanvasSetEvent>
    {
        protected CanvasGroup _canvasGroup;
        public LoadType loadCanvasType;

        [SerializeField] protected float _dummyTime = 3f;
        private float dummyProgress;
        protected string loadingCanvasKey;


        [Header("Events")]
        public UnityEvent OnLoadingStarted;
        public UnityEvent<float> OnLoadingProgressUpdate;
        public UnityEvent OnLoadingCompleted;
        public UnityEvent OnLoadingFailed;
        

        void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            this.Subscribe<LoadingEvents.LoadingCanvasSetEvent>();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            this.Unsubscribe<LoadingEvents.LoadingCanvasSetEvent>();
        }

        public override void DownloadProgress(AssetsDownloadStatus status)
        {
            base.DownloadProgress(status);
        }

        public override void LoadProgress(float progress)
        {
            //visibleProgress is the progress that will be shown to the user
            //it takes into account the dummy progress and the progress of the loading
            //if the progress is higher than the dummy progress, we show the dummy progress
            var visibleProgress = Mathf.Min(dummyProgress, progress);
            OnLoadingProgressUpdate?.Invoke(visibleProgress);
            if(visibleProgress >= 1f)
            EventManager.TriggerEvent(new LoadingEvents.VisibleLoadCompletedEvent());
        }

        protected override async UniTask OnLoadStarted(string address)
        {
            if(loadType == loadCanvasType)
            {
                await UniTask.WaitForEndOfFrame();
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
                await UniTask.WaitForEndOfFrame();
                Debug.Log("Loading Canvas Hide");
                _canvasGroup.blocksRaycasts = false;
                _canvasGroup.alpha = 0f;
                OnLoadingCompleted?.Invoke();
            }
        }

        protected async UniTask DummyLoad()
        {
            dummyProgress = 0;
            while(dummyProgress < 1f)
            {
                // Simulate loading progress
                // adding fractional progress to the dummy progress
                dummyProgress += Time.deltaTime / _dummyTime;
                OnLoadingProgressUpdate?.Invoke(dummyProgress);
                await UniTask.Yield();
            }
        }

        public void OnEventCallback(LoadingEvents.LoadingCanvasSetEvent eventType) => loadingCanvasKey = eventType.loadingCanvasKey;
    }
}
