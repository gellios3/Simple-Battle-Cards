using Models.Arena;
using strange.extensions.command.impl;
using Services;
using Signals.GameArena;

namespace Commands.GameArena
{
    public class InitBattleTurnCommand : Command
    {
        /// <summary>
        /// Init all decks signal
        /// </summary>
        [Inject]
        public InitAllDecksSignal InitAllDecksSignal { get; set; }

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
            // Init Active player 
            StateService.InitActivePlayer(BattleArena.ActiveSide == BattleSide.Player
                ? Arena.Player
                : Arena.Opponent);

            // init active turn
            // Increase turn count
            StateService.IncreaseTurnCount();

            // init turn history
            BattleArena.InitHistory();

            // ini card desk
            InitAllDecksSignal.Dispatch();
        }
    }
}