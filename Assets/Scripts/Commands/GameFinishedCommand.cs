using Models;
using Models.Arena;
using strange.extensions.command.impl;
using Signals;
using LogType = Models.LogType;

namespace Commands
{
    public class GameFinishedCommand : Command
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
        /// Add history log signal
        /// </summary>
        [Inject]
        public AddHistoryLogSignal AddHistoryLogSignal { get; set; }

        /// <summary>
        /// Execute event game finished
        /// </summary>
        public override void Execute()
        {
            var winPlayer = BattleArena.ActiveState == BattleState.YourTurn
                ? Arena.EnemyPlayer
                : Arena.YourPlayer;
            if (winPlayer.Status != PlayerStatus.Dead)
            {
                AddHistoryLogSignal.Dispatch(new[] {"\"", winPlayer.Name, "' WINS!"}, LogType.Battle);
            }
        }
    }
}