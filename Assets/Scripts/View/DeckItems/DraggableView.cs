using Models.Arena;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using View.GameArena;

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

        [SerializeField] private Transform _placeholder;

        public Transform Placeholder
        {
            get { return _placeholder; }
            set { _placeholder = value; }
        }

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

        private LayoutElement _layoutelem;

        public bool IsDragable = true;

        public bool IsDroppable = true;

        /// <summary>
        /// Create Stub
        /// </summary>
        /// <param name="width"></param>
        /// <param name="stubName"></param>
        public void CreatePlaceholder(float width, string stubName = "placeholder")
        {
            if (Placeholder != null) return;
            var placeholderGo = new GameObject {name = stubName};

            var rectTransform = placeholderGo.AddComponent<RectTransform>();
            rectTransform.localScale = Vector3.one;
            rectTransform.sizeDelta = new Vector2(width, 0);

            var le = placeholderGo.AddComponent<LayoutElement>();
            le.preferredWidth = width;
            le.preferredHeight = 0;
            le.flexibleWidth = 0;
            le.flexibleHeight = 0;

            Placeholder = placeholderGo.transform;
            Placeholder.SetParent(_placeholderParent);
        }

        /// <inheritdoc />
        /// <summary>
        /// On bigin drag
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            if (!IsDragable) return;
            ParentToReturnTo = transform.parent;
            PlaceholderParent = ParentToReturnTo;
            var dropableView = PlaceholderParent.GetComponent<DroppableView>();
            var width = GetComponent<LayoutElement>().preferredWidth;
            dropableView.CreateStub(width);
            dropableView.Stub.SetSiblingIndex(transform.GetSiblingIndex());

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
            if (!IsDragable)
                return;
            var pos = Camera.main.ScreenToWorldPoint(eventData.position);
            transform.position = new Vector3(pos.x, pos.y, 1);
            var dropableView = PlaceholderParent.GetComponent<DroppableView>();
            if (dropableView.Stub == null)
                return;

            var newSiblingIndex = PlaceholderParent.childCount;
            for (var i = 0; i < PlaceholderParent.childCount; i++)
            {
                if (!(transform.position.x < PlaceholderParent.GetChild(i).position.x))
                    continue;
                newSiblingIndex = i;
                if (dropableView.Stub.GetSiblingIndex() < newSiblingIndex)
                {
                    newSiblingIndex--;
                }

                break;
            }

            dropableView.Stub.SetSiblingIndex(newSiblingIndex);
        }

        /// <inheritdoc />
        /// <summary>
        /// On End drag card
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData eventData)
        {
            if (!IsDragable)
                return;
            transform.SetParent(ParentToReturnTo);
            var dropableView = PlaceholderParent.GetComponent<DroppableView>();
            if (dropableView.Stub != null)
            {
                transform.SetSiblingIndex(dropableView.Stub.GetSiblingIndex());
            }

            GetComponent<CanvasGroup>().blocksRaycasts = true;
            dropableView.DestroyStub();
        }


        /// <summary>
        /// Destroy мiew
        /// </summary>
        public void DestroyView()
        {
            PlaceholderParent.GetComponent<DroppableView>().DestroyStub();
            Destroy(gameObject);
        }
    }
}