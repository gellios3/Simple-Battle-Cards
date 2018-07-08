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
        public List<BattleItem> BattleHand { get; set; } = new List<BattleItem>();

        /// <summary>
        /// Active atack cards
        /// </summary>
        public List<BattleCard> ArenaCards { get; } = new List<BattleCard>();

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
        /// Player Status
        /// </summary>
        public PlayerStatus Status { get; private set; }

        /// <summary>
        /// Hand limit count
        /// </summary>
        public const int HandLimitCount = 6;
        
        /// <summary>
        /// Hand limit count
        /// </summary>
        public const int CartToAddCount = 3;

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
        /// <param name="cartDeck"></param>
        /// <param name="trateDeck"></param>
        public Player(CartDeck cartDeck, TrateDeck trateDeck)
        {
            Status = PlayerStatus.FistTurn;
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

    public enum PlayerStatus
    {
        Wait,
        Dead,
        FistTurn,
        Active
    }
}