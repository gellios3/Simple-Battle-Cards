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
        /// Init mana signal
        /// </summary>
        [Inject]
        public InitManaSignal InitManaSignal { get; set; }

        /// <summary>
        /// Init battle arena signal
        /// </summary>
        [Inject]
        public InitBattleArenaSignal InitBattleArenaSignal { get; set; }

        /// <summary>
        /// Init battle arena signal
        /// </summary>
        [Inject]
        public InitHandPanelSignal InitHandPanelSignal { get; set; }

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

            // On 2 Turn add more carts 
            BattleArena.CountOfCardsAddingToHand = Arena.CartToAddCount;
            if (StateService.TurnCount == 2)
            {
                BattleArena.CountOfCardsAddingToHand++;
            }

            // Init all desks
            InitAllDecksSignal.Dispatch();

            // Init mana view
            InitManaSignal.Dispatch();

            // Init hand panel
            InitHandPanelSignal.Dispatch();
            
            //Init battle arena
            InitBattleArenaSignal.Dispatch();
            
        }
    }
}