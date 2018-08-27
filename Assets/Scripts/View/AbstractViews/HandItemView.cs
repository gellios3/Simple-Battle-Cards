using DG.Tweening;
using Models.Arena;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View.AbstractViews
{
    public abstract class HandItemView : BaseItem, IPointerEnterHandler, IPointerExitHandler
    {

        /// <summary>
        /// Const
        /// </summary>
        private const float AnimationDelay = 0.2f;
        private const float MoveOffset = 0.4f;

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
            if (Placeholder == null)
                return;
            ZoomOutAnimation();
        }

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
            var wayPoints = new[] {PlaceholderParent.position, Placeholder.position};
            var path = transform.DOPath(wayPoints, 1, PathType.CatmullRom, PathMode.TopDown2D)
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

        /// <summary>
        /// Destroy view
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
            if (HasDraggable)
                return;
            if (Side == BattleSide.Player)
            {
                transform.DOMoveY(transform.position.y - MoveOffset, AnimationDelay);
            }
            else
            {
                transform.DOMoveY(transform.position.y + MoveOffset, AnimationDelay);
            }

            transform.DOScale(1, AnimationDelay).onComplete += () =>
            {
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
            if (Side == BattleSide.Player)
            {
                transform.DOMoveY(transform.position.y + MoveOffset, AnimationDelay);
            }
            else
            {
                transform.DOMoveY(transform.position.y - MoveOffset, AnimationDelay);
            }

            transform.DOScale(1.5f, AnimationDelay);
        }
    }
}