
using System.Collections.Generic;

namespace Models.State
{
    public class StatePlayer
    {
        /// <summary>
        /// Player name 
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Player Status
        /// </summary>
        public bool isActive { get;  set; }
        
        /// <summary>
        /// Battle player hand
        /// </summary>
        public List<StateItem> BattleHand { get; set; }
        
        /// <summary>
        /// Active atack cards
        /// </summary>
        public List<StateCard> ArenaCards { get; set; } 
        
        /// <summary>
        /// Random battle pul with cartd and trates
        /// </summary>
        public List<StateItem> BattlePull { get; set; }
    }
}