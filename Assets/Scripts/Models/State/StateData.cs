using System;

namespace Models.State
{
    [Serializable]
    public class StateData
    {
        /// <summary>
        /// Yor player
        /// </summary>
        public StatePlayer YourPlayer;

        /// <summary>
        /// State player
        /// </summary>
        public StatePlayer EnemyPlayer;
    }
}