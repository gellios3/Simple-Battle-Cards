using Models.ScriptableObjects;
using UnityEngine;

namespace Models.Arena
{
    public class BattleCard : BattleItem
    {
        /// <summary>
        /// Defence
        /// </summary>
        public int Defence { get; private set; }

        /// <summary>
        /// Attack
        /// </summary>
        public int Attack { get; private set; }

        /// <summary>
        /// Health
        /// </summary>
        public int Health { get; private set; }

        /// <summary>
        /// Source card
        /// </summary>
        public Card SourceCard { get; private set; }

        /// <summary>
        /// Card take damage and return is critial or not
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        public bool TakeDamage(int damage)
        {
            var isCriticalDamage = IsCritDamage();
            if (Defence > 0)
            {
                if (!isCriticalDamage)
                {
                    // Hit defence
                    Defence -= damage;
                    // Is defence less 0 hit health
                    if (Defence < 0)
                    {
                        damage += Defence;
                        Health -= Defence;
                    }
                    else
                    {
                        damage = 0;
                    }
                }
                else
                {
                    Defence = 0;
                    return true;
                }
            }

            // hit health
            if (Health > 0)
            {
                Health -= damage;
                if (isCriticalDamage)
                {
                    Health = 0;
                }
            }

            if (Health <= 0)
            {
                Status = BattleStatus.Dead;
            }

            return isCriticalDamage;
        }

        /// <summary>
        /// Is crit damage
        /// </summary>
        /// <returns></returns>
        private bool IsCritDamage()
        {
            var crit = Random.Range(0, 100);
            return crit <= 10;
        }

        /// <summary>
        /// Add trate to battle card
        /// </summary>
        /// <param name="trate"></param>
        public void AddTrate(BattleTrate trate)
        {
            Defence += trate.Defence;
            Health += trate.Health;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="card"></param>
        public BattleCard(Card card = null)
        {
            // init scriptable card to battle card
            if (card != null)
            {
                SourceCard = card;
                Defence = card.Defence;
                Attack = card.Attack;
                Health = card.Health;

                if (card.Trates.Count <= 0) return;

                foreach (var trate in card.Trates)
                {
                    Defence += trate.Defence;
                    Health += trate.Health;
                }
            }
            else // Create random card
            {
                Defence = Random.Range(1, 6);
                Attack = Random.Range(1, 6);
                Health = Random.Range(1, 6);
            }

            Status = BattleStatus.Wait;
        }
    }
}