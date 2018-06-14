using System;
using System.Collections.Generic;

namespace Models.State
{
    [Serializable]
    public class StateCard : StateItem
    {
        /// <summary>
        /// Defence
        /// </summary>
        public int Defence;

        /// <summary>
        /// Attack
        /// </summary>
        public int Attack;

        /// <summary>
        /// Health
        /// </summary>
        public int Health;

        /// <summary>
        /// Battle tarates
        /// </summary>
        public List<StateTrate> BattleTrates;
    }
}