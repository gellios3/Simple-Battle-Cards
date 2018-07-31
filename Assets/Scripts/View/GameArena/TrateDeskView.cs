using System.Collections;
using Models.Arena;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;
using View.GameItems;

namespace View.GameArena
{
    public class TrateDeskView : EventView
    {
        [SerializeField] private TextMeshProUGUI _trateDeckCountText;

        /// <summary>
        /// Placeholder parent
        /// </summary>
        [SerializeField] private Transform _placeholderParenTransform;

        [SerializeField] private GameObject _pull;
        [SerializeField] private Transform _handTransform;

        [SerializeField] private BattleSide _side;
        public BattleSide Side => _side;

        /// <summary>
        /// Arena
        /// </summary>
        [Inject]
        public Arena Arena { get; set; }

        /// <summary>
        /// Init deck count
        /// </summary>
        public void InitDeckCount()
        {
            if (Side == BattleSide.Player)
            {
                _trateDeckCountText.text = Arena.Player.TrateBattlePull.Count.ToString();
            }
            else if (Side == BattleSide.Opponent)
            {
                _trateDeckCountText.text = Arena.Opponent.TrateBattlePull.Count.ToString();
            }
        }

        /// <summary>
        /// Add trate to hand
        /// </summary>
        /// <param name="battleTrate"></param>
        /// <param name="side"></param>
        public void AddTrateToDeck(BattleTrate battleTrate, BattleSide side)
        {
            // Load Card
            var trateGameObject = (GameObject) Instantiate(
                Resources.Load("Prefabs/Trate", typeof(GameObject)), new Vector3(), Quaternion.identity,
                _pull.transform
            );
            // Set Z was zero position
            trateGameObject.transform.localPosition = new Vector3(trateGameObject.transform.localRotation.x,
                trateGameObject.transform.localRotation.y, 0);
            // Init Trate
            var trateView = trateGameObject.GetComponent<TrateView>();
            trateView.CanDroppable = false;
            trateView.Side = side;
            trateView.MainParenTransform = _placeholderParenTransform;
            trateView.PlaceholderParent = _handTransform;
            trateView.Init(battleTrate);
            trateView.CreatePlaceholder();
            trateGameObject.SetActive(false);
            InitDeckCount();
        }

        /// <summary>
        /// Add pull cards to hand
        /// </summary>
        public void AddPullTratesToHand()
        {
            StartCoroutine(HandOutTrate());
        }

        /// <summary>
        /// Hand out card animation
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private IEnumerator HandOutTrate(int pos = 0)
        {
            yield return new WaitForSeconds(0);

            if (_pull.transform.childCount == 0) yield break;
            pos++;
            var trateItem = _pull.transform.GetChild(0);
            var cardView = trateItem.GetComponent<TrateView>();
            trateItem.gameObject.SetActive(true);
            cardView.StartPathAnimation();
            StartCoroutine(HandOutTrate(pos));
        }
    }
}