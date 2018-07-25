using System.Collections;
using Models.Arena;
using strange.extensions.mediation.impl;
using Signals.GameArena;
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
        /// Init card deck signal
        /// </summary>
        [Inject]
        public InitTrateHandSignal InitTrateHandSignal { get; set; }

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
            cardView.PlaceholderParent = _handTransform;
            cardView.Init(battleCard);
            cardView.ToogleStubImage(false);
            cardView.CreatePlaceholder();
            cardGameObject.SetActive(false);
            InitDeckCount();
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
            yield return new WaitForSeconds(0);

            if (_pull.transform.childCount == 0)
            {
                InitTrateHandSignal.Dispatch();
                yield break;
            }

            pos++;
            var cardItem = _pull.transform.GetChild(0);
            var cardView = cardItem.GetComponent<CardView>();
            cardItem.gameObject.SetActive(true);
            cardView.StartPathAnimation();
            StartCoroutine(HandOutCard(pos));
        }

        /// <summary>
        /// Init deck count
        /// </summary>
        public void InitDeckCount()
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