using System;
using System.Collections.Generic;
using Services;
using Signals;

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
        public BattleTurnService ActiveBattleTurnService { get; private set; }

        /// <summary>
        /// Active state
        /// </summary>
        public BattleState ActiveState { get; set; }

        /// <summary>
        /// Battle history
        /// </summary>
        public readonly List<HistoryTurn> History = new List<HistoryTurn>();

        /// <summary>
        /// Init history
        /// </summary>
        public void InitHistory()
        {
            StateService.InitActiveHistoryTurn();
            History.Add(StateService.ActiveHistotyTurn);
        }

        /// <summary>
        /// Init battle turn
        /// </summary>
        public void InitActiveTurn()
        {
            AddToBattleHand();
            StateService.ActivePlayer.SetActiveStatus();
        }

        /// <summary>
        /// Fill Battle hand
        /// </summary>
        private void AddToBattleHand()
        {
            if (StateService.ActivePlayer.BattlePull.Count <= 0) return;

            StateService.ActivePlayer.BattleHand.Add(StateService.ActivePlayer.BattlePull[0]);

            var card = StateService.ActivePlayer.BattlePull[0] as BattleCard;
            if (card != null)
            {
                AddHistoryLogSignal.Dispatch(
                    new[] {"PLAYER \"", StateService.ActivePlayer.Name, "\" Add \"", card.SourceCard.name, "\" Card"},
                    LogType.Hand);
            }

            var trate = StateService.ActivePlayer.BattlePull[0] as BattleTrate;
            if (trate != null)
            {
                AddHistoryLogSignal.Dispatch(
                    new[]
                    {
                        "PLAYER \"", StateService.ActivePlayer.Name, "\" Add \"", trate.SourceTrate.name, "\" Trate"
                    }, LogType.Hand);
            }

            StateService.ActivePlayer.BattlePull.RemoveAt(0);
        }

        /// <summary>
        /// End turn
        /// </summary>
        public void EndTurn()
        {
            // Switch active state
            ActiveState = ActiveState == BattleState.YourTurn ? BattleState.EnemyTurn : BattleState.YourTurn;

            // Set active all not dead areana cards 
            foreach (var card in StateService.ActivePlayer.ArenaCards)
            {
                if (card.Status == BattleStatus.Dead) continue;
                if (card.Status != BattleStatus.Wait) continue;
                card.Status = BattleStatus.Active;
                // 
                AddHistoryLogSignal.Dispatch(
                    new[]
                    {
                        "PLAYER \"", StateService.ActivePlayer.Name, "\" Activate sleep \"", card.SourceCard.name,
                        "\" battle card!"
                    }, LogType.Battle);
            }

            // Set wait status
            StateService.ActivePlayer.SetWaitStatus();
        }

        /// <summary>
        /// Is game over
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool IsGameOver(Player player)
        {
            return player.BattlePull.Count == 0 &&
                   player.ArenaCards.FindAll(card => card.Status != BattleStatus.Dead).Count == 0;
        }
    }

    public enum BattleState
    {
        YourTurn,
        EnemyTurn
    }
}