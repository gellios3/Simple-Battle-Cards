using Models.Arena;
using strange.extensions.command.impl;
using Signals.GameArena;

namespace Commands.GameArena
{
    public class InitManaCommand : Command
    {
        /// <summary>
        /// Battle
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }

        /// <summary>
        /// Show mana signal
        /// </summary>
        [Inject]
        public ShowManaSignal ShowManaSignal { get; set; }

        /// <summary>
        /// Execute init mana
        /// </summary>
        public override void Execute()
        {
            var activePlayer = BattleArena.GetActivePlayer();
            // Init mana pull
            activePlayer.InitManaPull();
            ShowManaSignal.Dispatch(activePlayer.ManaPull);
        }
    }
}