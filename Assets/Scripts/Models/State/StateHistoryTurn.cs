using System;

namespace Models.State
{
    [Serializable]
    public class StateHistoryTurn
    {
        /// <summary>
        /// Hand log
        /// </summary>
        public string[] HandLog;

        /// <summary>
        /// Battle log
        /// </summary>
        public string[] BattleLog;
    }
}