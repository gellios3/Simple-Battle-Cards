using Models.Arena;
using strange.extensions.command.impl;
using Signals;
using Signals.GameArena;
using Signals.GameArena.CardSignals;
using UnityEngine;
using LogType = Models.LogType;

namespace Commands.GameArena
{
    public class EndTurnCommand : Command
    {
        /// <summary>
        /// Battle
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }

        /// <summary>
        /// Add history log signal
        /// </summary>
        [Inject]
        public RefreshHandSignal RefreshHandSignal { get; set; }

        /// <summary>
        /// Add history log signal
        /// </summary>
        [Inject]
        public InitBattleTurnSignal InitBattleTurnSignal { get; set; }

        /// <summary>
        /// Battle
        /// </summary>
        [Inject]
        public ActivateBattleCards ActivateBattleCards { get; set; }

        /// <summary>
        /// Execute end turn command
        /// </summary>
        public override void Execute()
        {
            RefreshHandSignal.Dispatch();
            
            // Activate battle cards 
            ActivateBattleCards.Dispatch();           

            // Switch active state
            BattleArena.ActiveSide =
                BattleArena.ActiveSide == BattleSide.Player ? BattleSide.Opponent : BattleSide.Player;

            // Init battle turn
            InitBattleTurnSignal.Dispatch();
        }
    }
}