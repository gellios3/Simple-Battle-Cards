using Models;
using Models.Arena;
using Signals;
using Signals.Arena;

namespace Services
{
    public class GenarateGameSessionService
    {

        /// <summary>
        /// Arena
        /// </summary>
        [Inject]
        public Arena Arena { get; set; }

        /// <summary>
        /// Battle
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }
        
        /// <summary>
        /// Make turn Signal
        /// </summary>
        [Inject] public MakeTurnSignal MakeTurnSignal { get; set; }
        
        /// <summary>
        /// End game signal
        /// </summary>
        [Inject] public EndGameSignal EndGameSignal { get; set; }

        /// <summary>
        /// Emulate Game
        /// </summary>
        public void EmulateGameSession()
        {
            var count = 0;
            while (true)
            {
                // init current player
                var player = BattleArena.ActiveState == BattleState.YourTurn
                    ? Arena.YourPlayer
                    : Arena.EnemyPlayer;

                if (count > 10)
                {
                    SaveGame();
                    break;
                }

                count++;

                if (!BattleArena.IsGameOver(player))
                {
                    MakeTurnSignal.Dispatch(player);
                    continue;
                }
                
                EndGameSignal.Dispatch();
                break;
            }
        }

        /// <summary>
        /// Save game
        /// </summary>
        public void SaveGame()
        {
            
        }
    }
}