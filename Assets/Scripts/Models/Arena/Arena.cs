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
        ///  Init Battle Arena 
        /// </summary>
        /// <param name="playerDeck"></param>
        /// <param name="enemyDeck"></param>
        public void Init(Deck playerDeck, Deck enemyDeck)
        {
            YourPlayer = new Player(playerDeck);
            EnemyPlayer = new Player(enemyDeck);
        }
    }
}