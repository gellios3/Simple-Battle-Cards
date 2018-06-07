using System.Collections.Generic;
using UnityEngine;

namespace Models.Arena
{
    public class BattleArena
    {
        /// <summary>
        /// Active state
        /// </summary>
        public BattleState ActiveState { get; set; }

        /// <summary>
        /// Battle history
        /// </summary>
        public readonly List<HistoryTurn> History = new List<HistoryTurn>();

        /// <summary>
        /// Turn history
        /// </summary>
        public HistoryTurn TurnHistoty { get; private set; }

        /// <summary>
        /// Batle turn
        /// </summary>
        public BattleTurn ActiveBattleTurn { get; private set; }

        /// <summary>
        /// Init history
        /// </summary>
        public void InitHistory()
        {
            TurnHistoty = new HistoryTurn();
            History.Add(TurnHistoty);
        }

        /// <summary>
        /// Init battle turn
        /// </summary>
        public void InitActiveTurn(Player player)
        {
            player.AddToBattleHand(TurnHistoty);
            player.Status = PlayerStatus.Active;
        }

        /// <summary>
        /// Init activa battle turn
        /// </summary>
        /// <param name="player"></param>
        public void InitActiveBattleTurn(Player player)
        {
            ActiveBattleTurn = new BattleTurn(player, TurnHistoty);
        }

        /// <summary>
        /// End turn
        /// </summary>
        public void EndTurn(Player player)
        {
            // Switch active state
            ActiveState = ActiveState == BattleState.YourTurn ? BattleState.EnemyTurn : BattleState.YourTurn;

            // Set active all not dead areana cards 
            foreach (var card in player.ArenaCards)
            {
                if (card.Status == BattleStatus.Dead) continue;
                if (card.Status != BattleStatus.Wait) continue;
                card.Status = BattleStatus.Active;
                TurnHistoty.AddBattleLog("PLAYER \"" + player.Name + "\" Activate sleep \"" + card.SourceCard.name + "\" battle card");
            }

            // Set wait status
            player.Status = PlayerStatus.Wait;
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