using Signals;
using Signals.GameArena;
using UnityEngine;
using View.GameArena;

namespace Mediators.GameArena
{
    public class HistoryLogMediator : TargetMediator<HistoryLogView>
    {
        /// <summary>
        /// Add history log to view signal
        /// </summary>
        [Inject]
        public AddHistoryLogToViewSignal AddHistoryLogToViewSignal { get; set; }

        /// <summary>
        /// Refresh history log
        /// </summary>
        [Inject]
        public RefreshHistoryLog RefreshHistoryLog { get; set; }

        /// <summary>
        /// On register mediator
        /// </summary>
        public override void OnRegister()
        {
            AddHistoryLogToViewSignal.AddListener(str => { View.AddHistoryLog(str); });

            RefreshHistoryLog.AddListener(() => { View.RefreshLog(); });
        }
    }
}