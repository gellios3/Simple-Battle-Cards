using Models.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Models.Arena
{
    public class BattleCard : BattleItem
    {
        /// <summary>
        /// Defence
        /// </summary>
        public int Defence { get; set; }

        /// <summary>
        /// Attack
        /// </summary>
        public int Attack { get; set; }

        /// <summary>
        /// Health
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Critical chance
        /// </summary>
        public int CriticalChance { get; set; }

        /// <summary>
        /// Critical hit
        /// </summary>
        public float CriticalHit { get; set; }

        /// <summary>
        /// Mana
        /// </summary>
        public int Mana { get; }

        /// <summary>
        /// Source card
        /// </summary>
        public Card SourceCard { get; }

        /// <summary>
        /// Critical damage
        /// </summary>
        public int CriticalDamage;

        /// <summary>
        /// Max trates count
        /// </summary>
        public const int MaxTratesCount = 3;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="card"></param>
        public BattleCard(Card card = null)
        {
            // init scribble card to battle card
            if (card != null)
            {
                SourceCard = card;
                Defence = card.Defence;
                Attack = card.Attack;
                Health = card.Health;
                CriticalChance = card.CriticalChance;
                CriticalHit = card.CriticalHit;
                Mana = card.Mana;
            }

            Status = BattleStatus.Wait;
        }

        /// <summary>
        /// Card take damage and return is critical or not
        /// </summary>
        /// <param name="hitCart"></param>
        /// <param name="enableCriticalDamage"></param>
        /// <returns></returns>
        public bool TakeDamage(BattleCard hitCart, bool enableCriticalDamage = true)
        {
            var damage = hitCart.Attack;
            var isCriticalDamage = true;
            if (enableCriticalDamage)
            {
                isCriticalDamage = IsCriticalDamage();
                if (isCriticalDamage)
                {
                    damage = (int) Mathf.Round(damage * hitCart.CriticalHit);
                    CriticalDamage = damage;
                }
            }

            if (Defence > 0)
            {
                // Hit defence
                Defence -= damage;
                // Is defence less 0 hit health
                if (Defence < 0)
                {
                    damage = Mathf.Abs(Defence);
                    Defence = 0;
                }
                else
                {
                    damage = 0;
                }
            }

            // hit health
            if (Health > 0)
            {
                Health -= damage;
            }

            if (Health <= 0)
            {
                Status = BattleStatus.Dead;
            }

            return isCriticalDamage;
        }

        /// <summary>
        /// Is critical damage
        /// </summary>
        /// <returns></returns>
        private bool IsCriticalDamage()
        {
            var range = Random.Range(0, 100);
            return range <= CriticalChance;
        }
    }
}