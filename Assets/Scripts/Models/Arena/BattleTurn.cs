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
        /// Battle turn
        /// </summary>
        /// <param name="player"></param>
        public BattleTurn(Player player)
        {
            ActivePlayer = player;
        }

        /// <summary>
        /// Add active card from hand
        /// </summary>
        /// <param name="card"></param>
        public void AddActiveCardFromHand(BattleCard card)
        {
            if (card.Status != CardStatus.Wait) return;
            ActivePlayer.ArenaCards.Add(new BattleCard(card.SourceCard));
            Debug.Log("Player " + ActivePlayer.Name + " Add card " + card.SourceCard.name + " to battle!");
        }

        /// <summary>
        /// Add trate to actice card
        /// </summary>
        /// <param name="card"></param>
        /// <param name="trate"></param>
        public void AddTrateToActiveCard(BattleCard card, BattleTrate trate)
        {
            if (trate.isUsed) return;
            card.AddTrate(new BattleTrate(trate.SourceTrate));
            Debug.Log("Player " + ActivePlayer.Name + " Add trate " + trate.SourceTrate.name + " to battle card " +
                      card.SourceCard.name);
        }

        /// <summary>
        /// Hit Enemy Card
        /// </summary>
        /// <param name="yourCard"></param>
        /// <param name="enemyCard"></param>
        public void HitEnemyCard(BattleCard yourCard, BattleCard enemyCard)
        {
            Debug.Log("Player " + ActivePlayer.Name + " Use Card " + yourCard.SourceCard.name + " hit ememy Card " +
                      enemyCard.SourceCard.name +
                      " take damage " + yourCard.Attack);
            enemyCard.TakeDamage(yourCard.Attack);
            Debug.Log("Enemy card " + enemyCard.SourceCard.name + " has " + enemyCard.Health + " Health and " +
                      enemyCard.Defence + " Defence");
        }
    }
}