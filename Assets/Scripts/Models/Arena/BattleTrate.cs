using Models.ScriptableObjects;
using UnityEngine;

namespace Models.Arena
{
    public class BattleTrate : BattleItem
    {
        /// <summary>
        /// Defence
        /// </summary>
        public int Defence { get; }

        /// <summary>
        /// Attack
        /// </summary>
        public int Health { get; }

        /// <summary>
        /// Attack
        /// </summary>
        public int Attack { get; }

        /// <summary>
        /// Mana
        /// </summary>
        public int Mana { get; }

        /// <summary>
        /// Critical chance
        /// </summary>
        public int CriticalChance { get; }

        /// <summary>
        /// Critical hit
        /// </summary>
        public float CriticalHit { get; }

        /// <summary>
        /// Source trate
        /// </summary>
        public Trate SourceTrate { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="trate"></param>
        public BattleTrate(Trate trate)
        {
            Status = BattleStatus.Wait;

            SourceTrate = trate;
            Defence = trate.Defence;
            Health = trate.Health;
            Attack = trate.Attack;
            Mana = trate.Mana;
            CriticalChance = trate.CriticalChance;
            CriticalHit = trate.CriticalHit;
        }
    }
}