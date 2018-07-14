using Models;
using Models.Arena;
using strange.extensions.command.impl;
using Services;
using Signals;

namespace Commands.GameArena
{
    public class InitBattleTurnCommand : Command
    {

        /// <summary>
        /// Battle
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }
        
        /// <summary>
        /// State service
        /// </summary>
        [Inject]
        public StateService StateService { get; set; }

        /// <summary>
        /// Arena
        /// </summary>
        [Inject]
        public Arena Arena { get; set; }

        /// <summary>
        /// Execute event init areana
        /// </summary>
        public override void Execute()
        {
            var player = BattleArena.ActiveState == BattleState.YourTurn
                ? Arena.Player
                : Arena.Opponent;
            StateService.InitActivePlayer(player);
            // init active turn
            BattleArena.InitActiveTurn();
        }
    }
}