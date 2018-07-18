using System;
using System.Collections.Generic;

namespace Models.State
{
    [Serializable]
    public class StatePlayer
    {
        /// <summary>
        /// Player name 
        /// </summary>
        public string Name;

        /// <summary>
        /// Player Status
        /// </summary>
        public bool isActive;

        /// <summary>
        /// Battle player hand
        /// </summary>
        public List<StateItem> BattleHand;

        /// <summary>
        /// Active atack cards
        /// </summary>
        public List<StateCard> ArenaCards;

        /// <summary>
        /// Random battle pul with cartd and trates
        /// </summary>
        public List<StateItem> BattlePull;
    }
}