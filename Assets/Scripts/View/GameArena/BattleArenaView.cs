using System;
using Models.Arena;
using UnityEngine;
using UnityEngine.EventSystems;
using View.AbstractViews;
using View.GameItems;

namespace View.GameArena
{
    public class BattleArenaView : DroppableView
    {
        /// <summary>
        /// On add trate to card
        /// </summary>
        public event Action<CardView> OnAddCatdToBattleArena;

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
                OnAddCatdToBattleArena?.Invoke(cardView);
            }
        }

        /// <summary>
        /// Add card unit to arena
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public BattleUnitView CreateCardUnit(BattleCard card)
        {
            // Load Card
            var cardGameObject = (GameObject) Instantiate(
                Resources.Load("Prefabs/BattleCardUnit", typeof(GameObject)),
                new Vector3(),
                Quaternion.identity,
                transform
            );
            var cardUnitView = cardGameObject.GetComponent<BattleUnitView>();
            cardUnitView.Init(card);
            cardUnitView.Side = Side;
            return cardUnitView;
        }

        /// <summary>
        /// On pointer enter
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;
            var draggableCard = eventData.pointerDrag.GetComponent<DraggableView>();
            if (draggableCard.CanDroppable)
            {
                base.OnPointerEnter(eventData);
            }
        }
    }
}