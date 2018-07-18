using System;
using Models.Arena;
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
        /// On add trate to card
        /// </summary>
        public event Action<DraggableView> OnAddViewToHand;

        /// <summary>
        /// Add card to hand
        /// </summary>
        /// <param name="battleCard"></param>
        /// <param name="side"></param>
        public void AddCardToHand(BattleCard battleCard, BattleSide side)
        {
            // Load Card
            var cardGameObject = (GameObject) Instantiate(
                Resources.Load("Prefabs/Card", typeof(GameObject)), new Vector3(), Quaternion.identity,
                transform
            );
            // Set Z was zero position
            cardGameObject.transform.localPosition = new Vector3(cardGameObject.transform.localRotation.x,
                cardGameObject.transform.localRotation.y, 0);
            // Init Card
            var cardView = cardGameObject.GetComponent<CardView>();
            cardView.Side = side;
            cardView.MainParenTransform = _placeholderParenTransform;
            cardView.Init(battleCard);
            OnAddViewToHand?.Invoke(cardView);
        }

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
            trateView.Side = side;
            trateView.MainParenTransform = _placeholderParenTransform;
            trateView.Init(battleTrate);
            OnAddViewToHand?.Invoke(trateView);
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
            if (draggableCard != null && draggableCard.IsDroppable)
            {
                draggableCard.PlaceholderParent = transform;
            }
        }

        /// <summary>
        /// On pointer exit
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;

            var draggableCard = eventData.pointerDrag.GetComponent<DraggableView>();
            if (draggableCard != null && draggableCard.IsDroppable && draggableCard.PlaceholderParent == transform )
            {
                draggableCard.PlaceholderParent = draggableCard.ParentToReturnTo;
            }
        }

        /// <summary>
        /// On Drop dragable View
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnDrop(PointerEventData eventData)
        {
            var draggableCard = eventData.pointerDrag.GetComponent<DraggableView>();
            if (draggableCard != null && draggableCard.IsDroppable)
            {
                draggableCard.ParentToReturnTo = transform;
            }
        }
    }
}