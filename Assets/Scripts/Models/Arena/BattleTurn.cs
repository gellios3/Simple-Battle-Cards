using UnityEngine;

namespace Models.Arena
{
    public class BattleTurn
    {
        /// <summary>
        /// Active player
        /// </summary>
        public Player ActivePlayer { get; private set; }

        /// <summary>
        /// History turn
        /// </summary>
        public HistoryTurn HistoryTurn { get; private set; }

        /// <summary>
        /// Battle turn
        /// </summary>
        /// <param name="player"></param>
        /// <param name="historyTurn"></param>
        public BattleTurn(Player player, HistoryTurn historyTurn)
        {
            HistoryTurn = historyTurn;
            ActivePlayer = player;
        }

        /// <summary>
        /// Add active card from hand
        /// </summary>
        /// <param name="card"></param>
        public void AddActiveCardFromHand(BattleCard card)
        {
            if (card.Status != BattleStatus.Wait) return;
            ActivePlayer.ArenaCards.Add(new BattleCard(card.SourceCard));
            // add history battle log
            HistoryTurn.AddBattleLog(
                "Player \"" + ActivePlayer.Name + "\" Add card \"" + card.SourceCard.name + "\" to battle!");
        }

        /// <summary>
        /// Add trate to actice card
        /// </summary>
        /// <param name="card"></param>
        /// <param name="trate"></param>
        public void AddTrateToActiveCard(BattleCard card, BattleTrate trate)
        {
            card.AddTrate(new BattleTrate(trate.SourceTrate));
            // add history battle log
            HistoryTurn.AddBattleLog("Player \"" + ActivePlayer.Name + "\" Add trate \"" + trate.SourceTrate.name +
                                     "\" to battle card \"" + card.SourceCard.name + "\"");
        }

        /// <summary>
        /// Hit Enemy Card
        /// </summary>
        /// <param name="yourCard"></param>
        /// <param name="enemyCard"></param>
        public void HitEnemyCard(BattleCard yourCard, BattleCard enemyCard)
        {
            HistoryTurn.AddBattleLog("Player \"" + ActivePlayer.Name + "\" Use Card \"" + yourCard.SourceCard.name +
                                     "\" hit ememy Card \"" + enemyCard.SourceCard.name + "\" take damage \"" +
                                     yourCard.Attack + "\"");
            enemyCard.TakeDamage(yourCard.Attack);
            if (enemyCard.Status == BattleStatus.Dead)
            {
                HistoryTurn.AddBattleLog("Enemy Card \"" + enemyCard.SourceCard.name + "\" has dead!");
            }
            else
            {
                HistoryTurn.AddBattleLog("Enemy card \"" + enemyCard.SourceCard.name + "\" has \"" + enemyCard.Health +
                                         "\" Health and \"" + enemyCard.Defence + "\" Defence");
            }
        }
    }
}