using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Debug = System.Diagnostics.Debug;

namespace View.AbstractViews
{
    public abstract class DraggableView : HandItemView, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public bool CanDraggable { private get; set; } = true;

        public bool CanDroppable { get; } = true;

        /// <inheritdoc />
        /// <summary>
        /// On begin drag
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            if (!CanDraggable)
                return;

            HasDraggable = true;
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
            if (!CanDraggable)
                return;

            if (Camera.main != null)
            {
                var pos = Camera.main.ScreenToWorldPoint(eventData.position);
                transform.position = new Vector3(pos.x, pos.y, 1);
            }

            if (Placeholder == null || PlaceholderParent == null)
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
            if (!CanDraggable)
                return;
            transform.SetParent(ParentToReturnTo);
            if (Placeholder != null)
            {
                transform.SetSiblingIndex(Placeholder.GetSiblingIndex());
                Destroy(Placeholder.gameObject);
            }

            HasDraggable = false;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
}