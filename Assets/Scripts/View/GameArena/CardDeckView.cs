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

        public BattleSide Side => _side;

        [SerializeField] private TextMeshProUGUI _cardDeckCountText;
        [SerializeField] private GameObject _pull;
        [SerializeField] private Transform _handTransform;

        /// <summary>
        /// Placeholder parent
        /// </summary>
        [SerializeField] private Transform _placeholderParenTransform;

        /// <summary>
        /// Arena
        /// </summary>
        [Inject]
        public Arena Arena { get; set; }

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
            cardGameObject.SetActive(false);
            InitCardDeckCount();
        }

        /// <summary>
        /// Add pull cards to hand
        /// </summary>
        public void AddPullCardsToHand()
        {
            StartCoroutine(HandOutCard());
        }

        /// <summary>
        /// Hand out card animation
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private IEnumerator HandOutCard(int pos = 0)
        {
            if (pos > 0)
            {
                yield return new WaitForSeconds(2);
            }
            else
            {
                yield return new WaitForSeconds(0);
            }

            if (_pull.transform.childCount == 0) yield break;
            pos++;
            var cardItem = _pull.transform.GetChild(0);
            cardItem.gameObject.SetActive(true);
            cardItem.GetComponent<CardView>().StartPathAnimation();
            StartCoroutine(HandOutCard(pos));
        }

        /// <summary>
        /// Init deck count
        /// </summary>
        public void InitCardDeckCount()
        {
            if (Side == BattleSide.Player)
            {
                _cardDeckCountText.text = Arena.Player.CardBattlePull.Count.ToString();
            }
            else if (Side == BattleSide.Opponent)
            {
                _cardDeckCountText.text = Arena.Opponent.CardBattlePull.Count.ToString();
            }
        }
    }
}