using Models;
using strange.extensions.command.impl;
using Services;

namespace Commands
{
    public class MakeTurnCommand : Command
    {
        /// <summary>
        /// Your CPU Behavior
        /// </summary>
        [Inject]
        public PlayerCpuBehaviorService PlayerCpu { get; set; }

        /// <summary>
        /// State service
        /// </summary>
        [Inject]
        public StateService StateService { get; set; }

        /// <summary>
        /// Current Player
        /// </summary>
        [Inject]
        public Player Player { get; set; }

        /// <summary>
        /// Execute event init areana
        /// </summary>
        public override void Execute()
        {
            // Init player Cpu Behavior
            StateService.InitActivePlayer(Player);
            PlayerCpu.InitTurn();
            PlayerCpu.MakeBattleTurn();
        }
    }
}