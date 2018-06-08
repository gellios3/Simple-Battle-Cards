namespace Models.State
{
    public class StateData
    {
        /// <summary>
        /// Yor player
        /// </summary>
        public StatePlayer YourPlayer { get; set; }
        
        /// <summary>
        /// State player
        /// </summary>
        public StatePlayer EnemyPlayer { get; set; }
    }
}