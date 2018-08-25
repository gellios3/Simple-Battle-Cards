using Models.Arena;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.EventSystems;

namespace View.AbstractViews
{
    public abstract class DroppableView : EventView, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private BattleSide _battleSide;

        public BattleSide Side => _battleSide;


        public abstract void OnDrop(PointerEventData eventData);

        /// <inheritdoc />
        /// <summary>
        /// On pointer enter
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;
            var draggableCard = eventData.pointerDrag.GetComponent<HandItemView>();
            if (draggableCard == null || draggableCard.Side != Side)
                return;
            draggableCard.PlaceholderParent = transform;
            draggableCard.CreatePlaceholder();          
        }

        /// <inheritdoc />
        /// <summary>
        /// On pointer exit
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;

            var draggableCard = eventData.pointerDrag.GetComponent<HandItemView>();

            if (draggableCard == null || draggableCard.PlaceholderParent != transform)
                return;
            if (draggableCard.Placeholder != null)
            {
                Destroy(draggableCard.Placeholder.gameObject);
            }

            draggableCard.PlaceholderParent = draggableCard.ParentToReturnTo;
        }
    }
}