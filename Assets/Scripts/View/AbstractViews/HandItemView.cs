using System;
using DG.Tweening;
using Models.Arena;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View.AbstractViews
{
    public abstract class HandItemView : EventView, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected TextMeshProUGUI NameText;
        [SerializeField] protected TextMeshProUGUI DescriptionText;
        [SerializeField] protected Image ArtworkImage;
        [SerializeField] protected TextMeshProUGUI ManaText;
        [SerializeField] protected TextMeshProUGUI AttackText;
        [SerializeField] protected TextMeshProUGUI HealthText;
        [SerializeField] protected TextMeshProUGUI DefenceText;

        public int Mana => Convert.ToInt32(ManaText.text);

        [SerializeField] private BattleSide _battleSide;

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

        public BattleSide Side
        {
            get { return _battleSide; }
            set { _battleSide = value; }
        }

        [SerializeField] private Transform _placeholder;

        public Transform Placeholder
        {
            get { return _placeholder; }
            private set { _placeholder = value; }
        }

        [SerializeField] private Transform _mainParenTransform;

        public Transform MainParenTransform
        {
            private get { return _mainParenTransform; }
            set { _mainParenTransform = value; }
        }

        protected bool HasDraggable;
        private bool _hasZoom;

        private const float AnimationDelay = 0.2f;
        private const float MoveoOffset = 0.4f;

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

            placeholderGo.transform.SetParent(PlaceholderParent);
            placeholderGo.transform.SetSiblingIndex(siblingIndex);

            Placeholder = placeholderGo.transform;
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
                HasDraggable = true;
                transform.SetParent(MainParenTransform);
            };
            path.onComplete += () =>
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                transform.SetParent(PlaceholderParent);
                transform.SetSiblingIndex(Placeholder.GetSiblingIndex());
                HasDraggable = false;
                Destroy(Placeholder.gameObject);
            };
        }

        /// <inheritdoc />
        /// <summary>
        /// On pointer enter
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            var draggableView = eventData.pointerDrag;
            if (Placeholder != null || draggableView != null || HasDraggable)
                return;

            ParentToReturnTo = transform.parent;

            CreatePlaceholder();
            transform.SetParent(MainParenTransform);
            ZoomInAnimation();
        }

        /// <inheritdoc />
        /// <summary>
        /// On pointer exit
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (HasDraggable || !_hasZoom)
                return;

            ZoomOutAnimation();
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
        /// Zoom out animation
        /// </summary>
        protected void ZoomOutAnimation()
        {
            if (!_hasZoom)
                return;
            if (Side == BattleSide.Player)
            {
                transform.DOMoveY(transform.position.y - MoveoOffset, AnimationDelay);
            }
            else
            {
                transform.DOMoveY(transform.position.y + MoveoOffset, AnimationDelay);
            }

            transform.DOScale(1, AnimationDelay).onComplete += () =>
            {
                _hasZoom = false;
                transform.SetParent(ParentToReturnTo);
                if (Placeholder == null) return;
                transform.SetSiblingIndex(Placeholder.GetSiblingIndex());
                Destroy(Placeholder.gameObject);
            };
        }

        /// <summary>
        /// Zoom in animation
        /// </summary>
        private void ZoomInAnimation()
        {
            transform.DOScale(1.5f, AnimationDelay).onComplete += () =>
            {
                if (Side == BattleSide.Player)
                {
                    transform.DOMoveY(transform.position.y + MoveoOffset, AnimationDelay);
                }
                else
                {
                    transform.DOMoveY(transform.position.y - MoveoOffset, AnimationDelay);
                }

                _hasZoom = true;
            };
        }
    }
}