using System;
using System.Collections;
using Models.Arena;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;
using View.DeckItems;

namespace View.GameArena
{
    public class CardDeckView : EventView
    {
        [SerializeField] private BattleSide _side;

        public BattleSide Side
        {
            get { return _side; }
            set { _side = value; }
        }

        [SerializeField] private TextMeshProUGUI _cardDeckCountText;
        [SerializeField] private GameObject _pull;
        [SerializeField] private Transform _handTransform;

        /// <summary>
        /// Placeholder parent
        /// </summary>
        [SerializeField] private Transform _placeholderParenTransform;

        /// <summary>
        /// On add trate to card
        /// </summary>
        public event Action<CardView> OnAddViewToDeck;


        /// <summary>
        /// Add card to hand
        /// </summary>
        /// <param name="battleCard"></param>
        /// <param name="side"></param>
        public void AddCardToDeck(BattleCard battleCard, BattleSide side)
        {
            // Load Card
            var cardGameObject = (GameObject) Instantiate(
                Resources.Load("Prefabs/Card", typeof(GameObject)), new Vector3(), Quaternion.identity,
                _pull.transform
            );
            // Set Z was zero position
            cardGameObject.transform.localPosition = new Vector3(cardGameObject.transform.localRotation.x,
                cardGameObject.transform.localRotation.y, 0);
            // Init Card
            var cardView = cardGameObject.GetComponent<CardView>();
            cardView.Side = side;
            cardView.MainParenTransform = _placeholderParenTransform;
            cardView.ParentToReturnTo = _handTransform;
            cardView.Init(battleCard);
            cardView.ToogleStubImage(false);
            OnAddViewToDeck?.Invoke(cardView);
        }

        private IEnumerator HandOutCard(CardView cardView)
        {
            yield return new WaitForSeconds(1);
            cardView.StartPathAnimation();
        }

        public void SetCardDeckCount(int count)
        {
            _cardDeckCountText.text = count.ToString();
        }
    }
}