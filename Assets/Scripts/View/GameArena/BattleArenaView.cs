﻿using System;
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
        public event Action<CardView> OnAddCartToBattleArena;

        /// <inheritdoc />
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
                OnAddCartToBattleArena?.Invoke(cardView);
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
                Vector3.zero,
                Quaternion.identity,
                transform
            );
            var cardUnitView = cardGameObject.GetComponent<BattleUnitView>();
            cardUnitView.Init(card);
            cardUnitView.Side = Side;
            cardUnitView.Card.Status = BattleStatus.Sleep;
            cardUnitView.HasActive = false;
            return cardUnitView;
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
            var draggableCard = eventData.pointerDrag.GetComponent<DraggableView>();
            if (draggableCard != null && draggableCard.CanDroppable)
            {
                base.OnPointerEnter(eventData);
            }
        }
    }
}