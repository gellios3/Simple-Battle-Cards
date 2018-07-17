namespace Models.Arena
{
    public abstract class BattleItem
    {
        /// <summary>
        /// Is card dead
        /// </summary>
        public BattleStatus Status { get; set; }
    }
    
    public enum BattleStatus
    {
        Wait,
        Active,
        Sleep,
        Dead
    }
}