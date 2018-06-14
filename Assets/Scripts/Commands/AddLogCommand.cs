using System.Text;
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
        public string[] CurrentLog { get; set; }

        /// <summary>
        /// Execute event add log
        /// </summary>
        public override void Execute()
        {
            var stringBuilder = new StringBuilder();
            foreach (var str in CurrentLog)
            {
                stringBuilder.Append(str);
            }

            StateService.ActiveHistotyTurn.AddLog(stringBuilder.ToString(), LogType);
        }
    }
}