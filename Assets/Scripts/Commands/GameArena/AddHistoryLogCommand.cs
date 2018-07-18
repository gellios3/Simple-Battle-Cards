using System.Text;
using strange.extensions.command.impl;
using Services;
using Signals.GameArena;
using UnityEngine;
using LogType = Models.LogType;

namespace Commands.GameArena
{
    public class AddHistoryLogCommand : Command
    {
        /// <summary>
        /// State service
        /// </summary>
        [Inject]
        public StateService StateService { get; set; }

        /// <summary>
        /// State service
        /// </summary>
        [Inject]
        public AddHistoryLogToViewSignal AddHistoryLogToViewSignal { get; set; }

        /// <summary>
        /// Log type
        /// </summary>
        [Inject]
        public LogType LogType { get; set; }

        /// <summary>
        /// Log type
        /// </summary>
        [Inject]
        public string[] CurrentLog { get; set; }

        /// <summary>
        /// Execute event add log
        /// </summary>
        public override void Execute()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("  ");
            foreach (var str in CurrentLog)
            {
                stringBuilder.Append(str);
            }
            stringBuilder.Append("\n");
            StateService.ActiveHistotyTurn.AddLog(stringBuilder.ToString(), LogType);
            AddHistoryLogToViewSignal.Dispatch(stringBuilder.ToString());
        }
    }
}