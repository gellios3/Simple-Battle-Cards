using Models.Arena;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.EventSystems;
using View.DeckItems;

namespace View.GameArena
{
    public class HandPanelView : EventView, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private BattleSide _battleSide;
        [SerializeField] private Transform _placeholderParenTransform;

        /// <summary>
        /// Get current Side
        /// </summary>
        /// <returns></returns>
        public BattleSide GetCurrentSide()
        {
            return _battleSide;
        }

        /// <summary>
        /// Add card to hand
        /// </summary>
        /// <param name="battleCard"></param>
        public void AddCardToHand(BattleCard battleCard)
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
            cardView.Init(battleCard, _placeholderParenTransform);
        }

        /// <summary>
        /// Add trate to hand
        /// </summary>
        /// <param name="battleTrate"></param>
        public void AddTrateToHand(BattleTrate battleTrate)
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
            trateView.Init(battleTrate, _placeholderParenTransform);
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;

            var draggableCard = eventData.pointerDrag.GetComponent<DraggableView>();
            if (draggableCard != null)
            {
                draggableCard.PlaceholderParent = transform;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;

            var draggableCard = eventData.pointerDrag.GetComponent<DraggableView>();
            if (draggableCard != null && draggableCard.PlaceholderParent == transform)
            {
                draggableCard.PlaceholderParent = draggableCard.ParentToReturnTo;
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            var draggableCard = eventData.pointerDrag.GetComponent<DraggableView>();
            if (draggableCard != null)
            {
                draggableCard.ParentToReturnTo = transform;
            }
        }
    }
}