using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View.DeckItems
{
    public abstract class DraggableView : EventView, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        
        [SerializeField] protected TextMeshProUGUI NameText;
        [SerializeField] protected TextMeshProUGUI DescriptionText;
        [SerializeField] protected Image ArtworkImage;
        [SerializeField] protected TextMeshProUGUI ManaText;
        [SerializeField] protected TextMeshProUGUI AttackText;
        [SerializeField] protected TextMeshProUGUI HealthText;
        [SerializeField] protected TextMeshProUGUI DefenceText;
        
        [SerializeField] protected Transform MainParenTransform;

        [SerializeField] private Transform _parentToReturnTo;

        public Transform ParentToReturnTo
        {
            get { return _parentToReturnTo; }
            set { _parentToReturnTo = value; }
        }

        [SerializeField] private Transform _placeholderParent;

        public Transform PlaceholderParent
        {
            get { return _placeholderParent; }
            set { _placeholderParent = value; }
        }

        protected GameObject Placeholder;
        private LayoutElement _layoutelem;
        
        public abstract void OnBeginDrag(PointerEventData eventData);
        public abstract void OnDrag(PointerEventData eventData);
        public abstract void OnEndDrag(PointerEventData eventData);
    }
}