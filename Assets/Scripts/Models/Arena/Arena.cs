using Models.ScriptableObjects;

namespace Models.Arena
{
    public class Arena
    {
        /// <summary>
        /// Your player
        /// </summary>
        public Player YourPlayer { get; private set; }

        /// <summary>
        /// Enemy player
        /// </summary>
        public Player EnemyPlayer { get; private set; }
        
        /// <summary>
        /// Hand limit count
        /// </summary>
        public const int HandLimitCount = 6;
        
        /// <summary>
        /// Hand limit count
        /// </summary>
        public const int CartToAddCount = 3;

        /// <summary>
        /// Arena rart count
        /// </summary>
        public const int ArenaCartCount = 6;
        
        /// <summary>
        /// Mana pull count
        /// </summary>
        public const int ManaPullCount = 5;

        /// <summary>
        ///  Init Battle Arena 
        /// </summary>
        /// <param name="playerCartDeck"></param>
        /// <param name="playerTrateDeck"></param>
        public void Init(CartDeck playerCartDeck, TrateDeck playerTrateDeck)
        {
            YourPlayer = new Player(playerCartDeck, playerTrateDeck);
            EnemyPlayer = new Player(playerCartDeck, playerTrateDeck);
        }
    }
}