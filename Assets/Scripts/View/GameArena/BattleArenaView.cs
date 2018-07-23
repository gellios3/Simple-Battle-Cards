using Models.Arena;
using Signals.GameArena.CardSignals;
using UnityEngine.EventSystems;
using View.DeckItems;

namespace View.GameArena
{
    public class BattleArenaView : DroppableView
    {
        /// <summary>
        /// Init card deck signal
        /// </summary>
        [Inject]
        public AddCatdToBattleArenaSignal AddCatdToBattleArenaSignal { get; set; }

        /// <summary>
        /// On Drop draggable Item
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnDrop(PointerEventData eventData)
        {
            var cardView = eventData.pointerDrag.GetComponent<CardView>();
            if (cardView == null || cardView.Card == null || cardView.PlaceholderParent != transform) return;
            if (cardView.Card.Status == BattleStatus.Wait)
            {
                AddCatdToBattleArenaSignal.Dispatch(cardView);
            }
        }

        public void AddCardViewToArena(CardView cardView)
        {
            cardView.IsDroppable = false;
            cardView.ParentToReturnTo = transform;
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;
            var draggableCard = eventData.pointerDrag.GetComponent<DraggableView>();
            if (draggableCard.IsDroppable)
            {
                base.OnPointerEnter(eventData);
            }
        }
    }
}