using Models;

namespace Services
{
    public class StateService
    {
        /// <summary>
        /// Active player
        /// </summary>
        public Player ActivePlayer { get; private set; }
        
        /// <summary>
        /// Turn history
        /// </summary>
        public HistoryTurn ActiveHistotyTurn { get; private set; }

        public void InitActiveHistoryTurn()
        {
            ActiveHistotyTurn = new HistoryTurn();
        }

        /// <summary>
        /// Init active player
        /// </summary>
        /// <param name="player"></param>
        public void InitActivePlayer(Player player)
        {
            ActivePlayer = player;
        }
    }
}