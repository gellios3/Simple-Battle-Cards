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
        /// Batle turn
        /// </summary>
        [Inject]
        public BattleTurnService ActiveBattleTurnService { get; set; }

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

//        /// <summary>
//        /// Is game over
//        /// </summary>
//        /// <param name="arenaPlayer"></param>
//        /// <returns></returns>
//        public bool IsGameOver(ArenaPlayer arenaPlayer)
//        {
//            return arenaPlayer.CardBattlePull.Count == 0 &&
//                   arenaPlayer.BattleHand.FindAll(item =>
//                   {
//                       var card = item as BattleCard;
//                       return card != null;
//                   }).Count == 0 &&
//                   arenaPlayer.ArenaCards.FindAll(card => card.Status != BattleStatus.Dead).Count == 0;
//        }
    }
    
    public enum BattleSide
    {
        Player,
        Opponent
    }
}