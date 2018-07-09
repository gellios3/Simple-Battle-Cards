using System.Collections.Generic;
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
        /// Critical chance
        /// </summary>
        public int CriticalChance { get; private set; }

        /// <summary>
        /// Critical hit
        /// </summary>
        public float CriticalHit { get; private set; }

        /// <summary>
        /// Mana
        /// </summary>
        public int Mana { get; }

        /// <summary>
        /// Source card
        /// </summary>
        public Card SourceCard { get; }

        /// <summary>
        /// Battle tarates
        /// </summary>
        public List<BattleTrate> BattleTrates { get; } = new List<BattleTrate>();

        /// <summary>
        /// Max tates count
        /// </summary>
        public const int MaxTratesCount = 3;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="card"></param>
        public BattleCard(Card card)
        {
            // init scriptable card to battle card
            SourceCard = card;
            Defence = card.Defence;
            Attack = card.Attack;
            Health = card.Health;
            CriticalChance = card.CriticalChance;
            CriticalHit = card.CriticalHit;
            Mana = card.Mana;
            Status = BattleStatus.Wait;
        }

        /// <summary>
        /// Card take damage and return is critial or not
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="hitCart"></param>
        /// <param name="enableCriticalDamage"></param>
        /// <returns></returns>
        public bool TakeDamage(BattleCard hitCart, bool enableCriticalDamage = true)
        {
            var damage = hitCart.Attack;
            var isCriticalDamage = false;
            if (enableCriticalDamage)
            {
                isCriticalDamage = IsCritDamage();

                if (isCriticalDamage)
                {
                    damage = Mathf.RoundToInt(damage * CriticalHit);
                }
            }

            if (Defence > 0)
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
        /// Is crit damage
        /// </summary>
        /// <returns></returns>
        private bool IsCritDamage()
        {
            var crit = Random.Range(0, 100);
            return crit <= CriticalChance;
        }

        /// <summary>
        /// Add trate to battle card
        /// </summary>
        /// <param name="trate"></param>
        public void AddTrate(BattleTrate trate)
        {        
            Defence += trate.Defence;
            Health += trate.Health;
            Attack += trate.Attack;
            CriticalChance += trate.CriticalChance;
            CriticalHit += trate.CriticalHit;
            BattleTrates.Add(trate);
        }
    }
}