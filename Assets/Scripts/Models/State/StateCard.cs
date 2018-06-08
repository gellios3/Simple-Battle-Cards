using System.Collections.Generic;
using Models.Arena;

namespace Models.State
{
    public class StateCard : StateItem
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
        /// Battle tarates
        /// </summary>
        public List<StateTrate> BattleTrates { get; set; }
    }
}