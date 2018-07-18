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
        public RefreshArenaSignal RefreshArenaSignal { get; set; }

        /// <summary>
        /// Add history log signal
        /// </summary>
        [Inject]
        public InitBattleTurnSignal InitBattleTurnSignal { get; set; }

        /// <summary>
        /// Battle
        /// </summary>
        [Inject]
        public ActivateBattleCardsSignal ActivateBattleCardsSignal { get; set; }

        /// <summary>
        /// Refresh history log
        /// </summary>
        [Inject]
        public RefreshHistoryLog RefreshHistoryLog { get; set; }

        /// <summary>
        /// Execute end turn command
        /// </summary>
        public override void Execute()
        {
            RefreshHandSignal.Dispatch();
            RefreshArenaSignal.Dispatch();
            
            // Activate battle cards 
            ActivateBattleCardsSignal.Dispatch();

            // Switch active state
            BattleArena.ActiveSide =
                BattleArena.ActiveSide == BattleSide.Player ? BattleSide.Opponent : BattleSide.Player;
            
            RefreshHistoryLog.Dispatch();

            // Init battle turn
            InitBattleTurnSignal.Dispatch();
        }
    }
}