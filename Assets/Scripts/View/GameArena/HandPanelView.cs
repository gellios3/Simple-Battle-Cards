using UnityEngine.EventSystems;
using View.AbstractViews;
using View.GameItems;

namespace View.GameArena
{
    public class HandPanelView : DroppableView
    {
        /// <summary>
        /// On Drop draggable View
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnDrop(PointerEventData eventData)
        {
//            var draggableCard = eventData.pointerDrag.GetComponent<DraggableView>();
//            if (draggableCard != null && draggableCard.CanDroppable && draggableCard.PlaceholderParent == transform)
//            {
//                draggableCard.ParentToReturnTo = transform;
//            }
        }

        /// <inheritdoc />
        /// <summary>
        /// On pointer enter
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;
            var draggableCard = eventData.pointerDrag.GetComponent<CardView>();

            if (draggableCard == null || !draggableCard.CanDroppable)
                return;
            base.OnPointerEnter(eventData);
        }
    }
}