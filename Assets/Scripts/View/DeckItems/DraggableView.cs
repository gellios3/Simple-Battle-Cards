using Models.Arena;
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

        [SerializeField] private Transform _mainParenTransform;

        public Transform MainParenTransform
        {
            get { return _mainParenTransform; }
            set { _mainParenTransform = value; }
        }

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

        [SerializeField] private BattleSide _battleSide;

        public BattleSide Side
        {
            get { return _battleSide; }
            set { _battleSide = value; }
        }

        public GameObject Placeholder { get; private set; }


        private LayoutElement _layoutelem;

        public bool IsDragable = true;

        public bool IsDroppable = true;

        protected void CreatePlaceholder()
        {
            Placeholder = new GameObject();

            var le = Placeholder.AddComponent<LayoutElement>();
            le.preferredWidth = GetComponent<LayoutElement>().preferredWidth;
            le.preferredHeight = 0;
            le.flexibleWidth = 0;
            le.flexibleHeight = 0;

            // Set plasholder zero height 
            Placeholder.GetComponent<RectTransform>().sizeDelta =
                new Vector2(GetComponent<LayoutElement>().preferredWidth, 0);
            Placeholder.transform.localScale = new Vector3(1, 1, 1);
        }

        /// <inheritdoc />
        /// <summary>
        /// On bigin drag
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            if (!IsDragable) return;
            CreatePlaceholder();
            Placeholder.transform.SetParent(transform.parent);
            Placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());

            ParentToReturnTo = transform.parent;
            PlaceholderParent = ParentToReturnTo;
            transform.SetParent(MainParenTransform);
            GetComponent<CanvasGroup>().blocksRaycasts = false;

            transform.position = new Vector3(eventData.position.x, eventData.position.y, 1);
        }

        /// <inheritdoc />
        /// <summary>
        /// On drag card
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrag(PointerEventData eventData)
        {
            if (!IsDragable) return;
            var pos = Camera.main.ScreenToWorldPoint(eventData.position);
            transform.position = new Vector3(pos.x, pos.y, 1);
            transform.position = eventData.position;
            if (Placeholder.transform.parent != Placeholder)
            {
                Placeholder.transform.SetParent(PlaceholderParent);
            }

            var newSiblingIndex = PlaceholderParent.childCount;
            for (var i = 0; i < PlaceholderParent.childCount; i++)
            {
                if (!(transform.position.x < PlaceholderParent.GetChild(i).position.x)) continue;
                newSiblingIndex = i;
                if (Placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                {
                    newSiblingIndex--;
                }

                break;
            }

            Placeholder.transform.SetSiblingIndex(newSiblingIndex);
        }

        /// <inheritdoc />
        /// <summary>
        /// On End drag card
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData eventData)
        {
            if (!IsDragable) return;
            transform.SetParent(ParentToReturnTo);
            transform.SetSiblingIndex(Placeholder.transform.GetSiblingIndex());
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            Destroy(Placeholder);
        }
    }
}