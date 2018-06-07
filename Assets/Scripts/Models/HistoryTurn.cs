using Boo.Lang;

namespace Models
{
    public class HistoryTurn
    {
        /// <summary>
        /// Hand log
        /// </summary>
        public readonly List<string> HandLog = new List<string>();
        
        /// <summary>
        /// Battle log
        /// </summary>
        public readonly List<string> BattleLog = new List<string>();

        /// <summary>
        /// Add battle log
        /// </summary>
        /// <param name="str"></param>
        public void AddBattleLog(string str)
        {
            BattleLog.Add(str);
        }

        /// <summary>
        /// Add hand log
        /// </summary>
        /// <param name="str"></param>
        public void AddHandLog(string str)
        {
            HandLog.Add(str);
        }
    }
}