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
        public List<HistoryItem> History { get; private set; }

        /// <summary>
        /// Batle turn
        /// </summary>
        public BattleTurn ActiveBattleTurn { get; private set; }

        /// <summary>
        /// Init battle turn
        /// </summary>
        public void InitActiveTurn(Player player)
        {
            player.AddToBattleHand();
            player.Status = PlayerStatus.Active;
        }

        /// <summary>
        /// Init activa battle turn
        /// </summary>
        /// <param name="player"></param>
        public void InitActiveBattleTurn(Player player)
        {
            ActiveBattleTurn = new BattleTurn(player);
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
                if (card.Status == CardStatus.Dead) continue;
                if (card.Status != CardStatus.Wait) continue;
                card.Status = CardStatus.Active;
                Debug.Log("PLAYER " + player.Name + " Activate " + card.SourceCard.name + " battle card");
            }

            // Set wait status
            player.Status = PlayerStatus.Wait;
            Debug.Log("END " + player.Name + " turn");
        }

        /// <summary>
        /// Is game over
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool IsGameOver(Player player)
        {
            return player.BattlePull.Count == 0 &&
                   player.ArenaCards.FindAll(card => card.Status != CardStatus.Dead).Count == 0;
        }
    }

    public enum BattleState
    {
        YourTurn,
        EnemyTurn
    }
}