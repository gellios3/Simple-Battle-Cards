using System;
using System.Collections.Generic;
using Models.Arena;
using Models.ScriptableObjects;
using Services;
using UnityEngine;
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
        public List<BattleItem> BattleHand { get; set; } = new List<BattleItem>();

        /// <summary>
        /// Active atack cards
        /// </summary>
        public List<BattleCard> ArenaCards { get; } = new List<BattleCard>();

        /// <summary>
        /// Random battle pul with cartd and trates
        /// </summary>
        public List<BattleItem> BattlePull { get; } = new List<BattleItem>();

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
        public PlayerStatus Status { get; private set; }

        /// <summary>
        /// Pull type
        /// </summary>
        private enum PullType
        {
            Card,
            Trate
        }

        /// <summary>
        /// Wait player
        /// </summary>
        public void SetWaitStatus()
        {
            Status = PlayerStatus.Wait;
        }

        /// <summary>
        /// Activate Player
        /// </summary>
        public void SetActiveStatus()
        {
            Status = PlayerStatus.Active;
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
                            BattlePull.Add(new BattleCard(deck.Cards[_cardPositions[cardCount]]));
                            cardCount++;
                        }
                        else if (trateCount < deck.Trates.Count)
                        {
                            BattlePull.Add(new BattleTrate(deck.Trates[_tratePositions[trateCount]]));
                            trateCount++;
                        }

                        break;
                    case PullType.Trate:
                        if (trateCount < deck.Trates.Count)
                        {
                            BattlePull.Add(new BattleTrate(deck.Trates[_tratePositions[trateCount]]));
                            trateCount++;
                        }
                        else if (cardCount < deck.Cards.Count)
                        {
                            BattlePull.Add(new BattleCard(deck.Cards[_cardPositions[cardCount]]));
                            cardCount++;
                        }

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
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