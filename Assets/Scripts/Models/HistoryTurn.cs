using System;
using System.Collections.Generic;

namespace Models
{
    public class HistoryTurn
    {
        /// <summary>
        /// Hand log
        /// </summary>
        public readonly List<string> HandLog = new List<string>();

        /// <summary>
        /// Hand log
        /// </summary>
        public readonly List<string> GeneralLog = new List<string>();

        /// <summary>
        /// Battle log
        /// </summary>
        public readonly List<string> BattleLog = new List<string>();

        /// <summary>
        /// Battle log
        /// </summary>
        public readonly List<string> ErrorLog = new List<string>();

        /// <summary>
        /// Add History log
        /// </summary>
        /// <param name="str"></param>
        /// <param name="type"></param>
        public void AddLog(string str, LogType type)
        {
            switch (type)
            {
                case LogType.Battle:
                    BattleLog.Add(str);
                    break;
                case LogType.Hand:
                    HandLog.Add(str);
                    break;
                case LogType.General:
                    GeneralLog.Add(str);
                    break;
                case LogType.Error:
                    ErrorLog.Add(str);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }

    public enum LogType
    {
        General,
        Battle,
        Error,
        Hand
    }
}