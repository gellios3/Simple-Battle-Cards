using System.Collections.Generic;
using Models.Arena;
using Models.ScriptableObjects;
using UnityEngine;

namespace Models
{
    public class ArenaPlayer
    {
        /// <summary>
        /// Player name 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Random battle pul with cartd 
        /// </summary>
        public List<BattleCard> CardBattlePull { get; } = new List<BattleCard>();

        /// <summary>
        /// Random battle pul with  trates
        /// </summary>
        public List<BattleTrate> TrateBattlePull { get; } = new List<BattleTrate>();

        /// <summary>
        /// Random card positions
        /// </summary>
        private readonly List<int> _cardPositions = new List<int>();

        /// <summary>
        /// Random trate positions 
        /// </summary>
        private readonly List<int> _tratePositions = new List<int>();

        /// <summary>
        /// Mana pull
        /// </summary>
        public int ManaPull { get; private set; }

        /// <summary>
        /// Is CPU
        /// </summary>
        public bool IsCpu { get; set; }

        /// <summary>
        /// InitManaPull
        /// </summary>
        public void InitManaPull()
        {
            ManaPull = Arena.Arena.ManaPullCount;
        }

        /// <summary>
        /// Less mana pull
        /// </summary>
        /// <param name="lessCount"></param>
        public bool LessManaPull(int lessCount)
        {
            if (ManaPull < lessCount) return false;
            ManaPull -= lessCount;
            return true;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cartDeck"></param>
        /// <param name="trateDeck"></param>
        public ArenaPlayer(CartDeck cartDeck, TrateDeck trateDeck)
        {
            // generate random positions
            InitRandomPositions(_cardPositions, cartDeck.Cards.Count);
            InitRandomPositions(_tratePositions, trateDeck.Trates.Count);

            var cardCount = 0;
            var trateCount = 0;
            // fill card battle pull
            foreach (var card in cartDeck.Cards)
            {
                if (cardCount >= cartDeck.Cards.Count) continue;
                CardBattlePull.Add(new BattleCard(cartDeck.Cards[_cardPositions[cardCount]]));
                cardCount++;
            }

            // fill trate battle pull
            foreach (var card in trateDeck.Trates)
            {
                if (trateCount >= trateDeck.Trates.Count) continue;
                TrateBattlePull.Add(new BattleTrate(trateDeck.Trates[_tratePositions[trateCount]]));
                trateCount++;
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
}