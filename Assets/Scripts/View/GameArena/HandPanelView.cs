﻿using Models.Arena;
using UnityEngine;
using UnityEngine.EventSystems;
using View.DeckItems;

namespace View.GameArena
{
    public class HandPanelView : DroppableView
    {
        /// <summary>
        /// Placeholder parent
        /// </summary>
        [SerializeField] private Transform _placeholderParenTransform;

        /// <summary>
        /// Add trate to hand
        /// </summary>
        /// <param name="battleTrate"></param>
        /// <param name="side"></param>
        public void AddTrateToHand(BattleTrate battleTrate, BattleSide side)
        {
            // Load Card
            var trateGameObject = (GameObject) Instantiate(
                Resources.Load("Prefabs/Trate", typeof(GameObject)), new Vector3(), Quaternion.identity,
                transform
            );
            // Set Z was zero position
            trateGameObject.transform.localPosition = new Vector3(trateGameObject.transform.localRotation.x,
                trateGameObject.transform.localRotation.y, 0);
            // Init Trate
            var trateView = trateGameObject.GetComponent<TrateView>();
            trateView.IsDroppable = false;
            trateView.Side = side;
            trateView.MainParenTransform = _placeholderParenTransform;
            trateView.Init(battleTrate);
        }

        /// <summary>
        /// On Drop dragable View
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnDrop(PointerEventData eventData)
        {
            var draggableCard = eventData.pointerDrag.GetComponent<DraggableView>();
            if (draggableCard != null && draggableCard.IsDroppable && draggableCard.PlaceholderParent == transform)
            {
                draggableCard.ParentToReturnTo = transform;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// On poiter enter
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;
            var draggableCard = eventData.pointerDrag.GetComponent<DraggableView>();

            if (draggableCard as CardView)
            {
                if (!draggableCard.IsDroppable) return;
                base.OnPointerEnter(eventData);
            }
            else if (draggableCard as TrateView)
            {
                base.OnPointerEnter(eventData);
            }
        }
    }
}