using System;
using System.Collections.Generic;
using Models.Arena;
using Models.ScriptableObjects;
using Random = UnityEngine.Random;

namespace Models
{
    public class Player
    {
        /// <summary>
        /// Player name 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Battle player hand
        /// </summary>
        private List<BattleItem> _battleHand = new List<BattleItem>();

        public List<BattleItem> BattleHand
        {
            get { return _battleHand; }
            set { _battleHand = value; }
        }

        /// <summary>
        /// Active atack cards
        /// </summary>
        private readonly List<BattleCard> _arenaCards = new List<BattleCard>();

        public List<BattleCard> ArenaCards
        {
            get { return _arenaCards; }
        }

        /// <summary>
        /// Random battle pul with cartd and trates
        /// </summary>
        private readonly List<BattleItem> _battlePull = new List<BattleItem>();

        public List<BattleItem> BattlePull
        {
            get { return _battlePull; }
        }

        /// <summary>
        /// Random card positions
        /// </summary>
        private readonly List<int> _cardPositions = new List<int>();

        /// <summary>
        /// Random trate positions 
        /// </summary>
        private readonly List<int> _tratePositions = new List<int>();

        /// <summary>
        /// Player Status
        /// </summary>
        public PlayerStatus Status { get; set; }


        /// <summary>
        /// Pull type
        /// </summary>
        private enum PullType
        {
            Card,
            Trate
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="deck"></param>
        public Player(Deck deck)
        {
            Status = PlayerStatus.FistTurn;
            // generate random positions
            InitRandomPositions(_cardPositions, deck.Cards.Count);
            InitRandomPositions(_tratePositions, deck.Trates.Count);
            var cardCount = 0;
            var trateCount = 0;
            // fill battle pull
            for (var i = 0; i < deck.Cards.Count + deck.Trates.Count; i++)
            {
                // get random type
                var randomType = Random.Range(0, 2) == 0 ? PullType.Card : PullType.Trate;
                switch (randomType)
                {
                    case PullType.Card:
                        if (cardCount < deck.Cards.Count)
                        {
                            _battlePull.Add(new BattleCard(deck.Cards[_cardPositions[cardCount]]));
                            cardCount++;
                        }
                        else if (trateCount < deck.Trates.Count)
                        {
                            _battlePull.Add(new BattleTrate(deck.Trates[_tratePositions[trateCount]]));
                            trateCount++;
                        }

                        break;
                    case PullType.Trate:
                        if (trateCount < deck.Trates.Count)
                        {
                            _battlePull.Add(new BattleTrate(deck.Trates[_tratePositions[trateCount]]));
                            trateCount++;
                        }
                        else if (cardCount < deck.Cards.Count)
                        {
                            _battlePull.Add(new BattleCard(deck.Cards[_cardPositions[cardCount]]));
                            cardCount++;
                        }

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Fill Battle hand
        /// </summary>
        public void AddToBattleHand(HistoryTurn historyTurn)
        {
            if (_battlePull.Count <= 0) return;

            BattleHand.Add(_battlePull[0]);

            var card = _battlePull[0] as BattleCard;
            if (card != null)
            {
                historyTurn.AddHandLog("Player \"" + Name + "\" Add \"" + card.SourceCard.name + "\" Card");
            }

            var trate = _battlePull[0] as BattleTrate;
            if (trate != null)
            {
                historyTurn.AddHandLog("Player \"" + Name + "\" Add \"" + trate.SourceTrate.name + "\" Trate");
            }

            _battlePull.RemoveAt(0);
        }

        /// <summary>
        /// Init random unique positions
        /// </summary>
        /// <param name="positions"></param>
        /// <param name="count"></param>
        private static void InitRandomPositions(ICollection<int> positions, int count)
        {
            while (true)
            {
                var randomPos = Random.Range(0, count);
                if (!positions.Contains(randomPos))
                {
                    positions.Add(randomPos);
                }

                if (positions.Count < count)
                {
                    continue;
                }

                break;
            }
        }
    }

    public enum PlayerStatus
    {
        Wait,
        Dead,
        FistTurn,
        Active
    }
}