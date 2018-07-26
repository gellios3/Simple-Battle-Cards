using DG.Tweening;
using Models.Arena;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View.AbstractViews
{
    public abstract class DraggableView : EventView, IBeginDragHandler, IDragHandler, IEndDragHandler,
        IPointerEnterHandler, IPointerExitHandler
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

        public bool IsDragable = true;
        public bool HasDragable;
        public bool HasZoom;
        public bool IsDroppable = true;

        /// <summary>
        /// Create Stub
        /// </summary>
        /// <param name="stubName"></param>
        public void CreatePlaceholder(string stubName = "placeholder")
        {
            if (Placeholder != null)
                return;
            var siblingIndex = transform.GetSiblingIndex();
            var width = GetComponent<LayoutElement>().preferredWidth;
            var placeholderGo = new GameObject {name = stubName};

            var rectTransform = placeholderGo.AddComponent<RectTransform>();
            rectTransform.localScale = Vector3.one;
            rectTransform.sizeDelta = new Vector2(width, 0);

            var le = placeholderGo.AddComponent<LayoutElement>();
            le.preferredWidth = width;
            le.preferredHeight = 0;
            le.flexibleWidth = 0;
            le.flexibleHeight = 0;

            placeholderGo.transform.SetParent(_placeholderParent);
            placeholderGo.transform.SetSiblingIndex(siblingIndex);

            Placeholder = placeholderGo.transform;
        }

        /// <inheritdoc />
        /// <summary>
        /// On bigin drag
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            if (!IsDragable)
                return;

            HasDragable = true;
            transform.DOScale(1, 0.3f);

            PlaceholderParent = ParentToReturnTo;

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
            if (Placeholder == null)
                return;

            var newSiblingIndex = PlaceholderParent.childCount;
            for (var i = 0; i < PlaceholderParent.childCount; i++)
            {
                if (!(transform.position.x < PlaceholderParent.GetChild(i).position.x))
                    continue;
                newSiblingIndex = i;
                if (Placeholder.GetSiblingIndex() < newSiblingIndex)
                {
                    newSiblingIndex--;
                }

                break;
            }

            Placeholder.SetSiblingIndex(newSiblingIndex);
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
            if (Placeholder != null)
            {
                transform.SetSiblingIndex(Placeholder.GetSiblingIndex());
                Destroy(Placeholder.gameObject);
            }

            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        /// <inheritdoc />
        /// <summary>
        /// On pointer enter
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            var draggableView = eventData.pointerDrag;
            if (Placeholder != null || draggableView != null || HasDragable)
                return;

            Debug.Log("OnPointerEnter");
            ParentToReturnTo = transform.parent;

            CreatePlaceholder();
            transform.SetParent(MainParenTransform);
            transform.DOScale(1.5f, 0.3f).onComplete += () =>
            {
                if (Side == BattleSide.Player)
                {
                    transform.DOMoveY(transform.position.y + 0.4f, 0.3f);
                }
                else
                {
                    transform.DOMoveY(transform.position.y - 0.4f, 0.3f);
                }

                HasZoom = true;
            };
        }

        /// <inheritdoc />
        /// <summary>
        /// On pointer exit
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (HasDragable || !HasZoom)
                return;
            Debug.Log("OnPointerExit");

            if (Side == BattleSide.Player)
            {
                transform.DOMoveY(transform.position.y - 0.4f, 0.3f);
            }
            else
            {
                transform.DOMoveY(transform.position.y + 0.4f, 0.3f);
            }

            transform.DOScale(1, 0.3f).onComplete += () =>
            {
                HasZoom = false;
                transform.SetParent(ParentToReturnTo);
                if (Placeholder == null) return;
                transform.SetSiblingIndex(Placeholder.GetSiblingIndex());
                Destroy(Placeholder.gameObject);
            };
        }


        /// <summary>
        /// Destroy мiew
        /// </summary>
        public void DestroyView()
        {
            Destroy(gameObject);
            if (Placeholder != null)
            {
                Destroy(Placeholder.gameObject);
            }
        }

        /// <summary>
        /// Start path animation
        /// </summary>
        public void StartPathAnimation()
        {
            var waypoints = new[] {PlaceholderParent.position, Placeholder.position};
            var path = transform.DOPath(waypoints, 1, PathType.CatmullRom, PathMode.TopDown2D)
                .SetOptions(false, AxisConstraint.Z);
            path.onPlay += () =>
            {
                HasDragable = true;
                transform.SetParent(MainParenTransform);
            };
            path.onComplete += () =>
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                transform.SetParent(PlaceholderParent);
                transform.SetSiblingIndex(Placeholder.GetSiblingIndex());
                HasDragable = false;
                Destroy(Placeholder.gameObject);
            };
        }
    }
}