using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Conkist.UI
{
    [RequireComponent(typeof (IPointerClickHandler))]
    public class DialogOptionHandler : MonoBehaviour
    {
        [SerializeField] TMP_Text _tmpOption;

        public string OptionText { get => _tmpOption.text; set => _tmpOption.SetText(value); }

        private IPointerClickHandler _clickHandler;

        public UnityAction OptionAction { get; set; }

        void Awake()
        {
            _clickHandler = GetComponent<IPointerClickHandler>();
        }

        public void ExecuteOption()
        {
            OptionAction?.Invoke();
        }
    }
}
