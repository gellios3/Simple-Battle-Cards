using Models;
using strange.extensions.command.impl;
using Services;

namespace Commands
{
    public class AddLogCommand : Command
    {
        /// <summary>
        /// State service
        /// </summary>
        [Inject]
        public StateService StateService { get; set; }

        /// <summary>
        /// Log type
        /// </summary>
        [Inject]
        public LogType LogType { get; set; }

        /// <summary>
        /// Log type
        /// </summary>
        [Inject]
        public string CurrentLog { get; set; }

        /// <summary>
        /// Execute event game finished
        /// </summary>
        public override void Execute()
        {
            StateService.ActiveHistotyTurn.AddLog(CurrentLog, LogType);
        }
    }
}