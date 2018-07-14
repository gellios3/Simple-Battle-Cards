using Signals.GameArena;
using UnityEngine;
using View.GameArena;

namespace Mediators.GameArena
{
    public class HistoryLogMediator : TargetMediator<HistoryLogView>
    {
        /// <summary>
        /// State service
        /// </summary>
        [Inject]
        public AddHistoryLogToViewSignal AddHistoryLogToViewSignal { get; set; }

        /// <summary>
        /// On register mediator
        /// </summary>
        public override void OnRegister()
        {
            AddHistoryLogToViewSignal.AddListener(str =>
            {
                View.AddHistoryLog(str);
            });
        }
    }
}