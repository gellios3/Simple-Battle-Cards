using Signals.GameArena;
using UnityEngine;
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
            if (cardView != null)
            {
                AddCatdToBattleArenaSignal.Dispatch(cardView);
            }
        }

        public void AddCardViewToArena(CardView cardView)
        {
            cardView.ParentToReturnTo = transform;
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("BattleArenaView OnPointerEnter");
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("BattleArenaView OnPointerExit");
        }
    }
}