using System;
using System.Collections.Generic;
using Services;
using Signals;
using Random = UnityEngine.Random;

namespace Models.Arena
{
    [Serializable]
    public class BattleArena
    {
        /// <summary>
        /// State service
        /// </summary>
        [Inject]
        public StateService StateService { get; set; }

        /// <summary>
        /// Add history log signal
        /// </summary>
        [Inject]
        public AddHistoryLogSignal AddHistoryLogSignal { get; set; }

        /// <summary>
        /// Active state
        /// </summary>
        public BattleSide ActiveSide { get; set; }

        /// <summary>
        /// Battle history
        /// </summary>
        public readonly List<HistoryTurn> History = new List<HistoryTurn>();

        /// <summary>
        /// Count of cards adding to hand
        /// </summary>
        public int CountOfCardsAddingToHand { get; set; }

        public int HandCount;
        public int HandCardsCount;
        public int ArenaCardsCount;

        /// <summary>
        /// Init history
        /// </summary>
        public void InitHistory()
        {
            StateService.InitActiveHistoryTurn();
            History.Add(StateService.ActiveHistotyTurn);
            AddHistoryLogSignal.Dispatch(new[] {"INIT '", StateService.TurnCount.ToString(), "' TURN!"},
                LogType.General);
        }

        /// <summary>
        /// Get arena player
        /// </summary>
        /// <returns></returns>
        public ArenaPlayer GetActivePlayer()
        {
            return StateService.ActiveArenaPlayer;
        }

        /// <summary>
        /// Is game over
        /// </summary>
        /// <returns></returns>
        public bool IsGameOver()
        {
            return GetActivePlayer().CardBattlePull.Count == 0 && HandCardsCount == 0 && ArenaCardsCount == 0;
        }
    }

    public enum BattleSide
    {
        Player,
        Opponent
    }
}