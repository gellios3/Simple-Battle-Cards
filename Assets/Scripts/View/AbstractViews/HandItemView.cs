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
            protected set { _placeholder = value; }
        }
        
        [SerializeField] private Transform _mainParenTransform;

        public Transform MainParenTransform
        {
            get { return _mainParenTransform; }
            set { _mainParenTransform = value; }
        }
        
        public bool HasDraggable;
        public bool HasZoom;
        
        
        
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
            if (HasDraggable || !HasZoom)
                return;

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
    }
}