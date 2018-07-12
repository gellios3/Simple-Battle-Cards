using strange.extensions.mediation.impl;
using Signals;
using Signals.Arena;
using UnityEngine;

namespace View
{
    public class GameArenaView : EventView
    {
        /// <summary>
        /// Save log signal
        /// </summary>
        [Inject]
        public SaveLogSignal SaveLogSignal { get; set; }

        public void OnArenaInitialized()
        {
            Debug.Log("OnInitArena");
            SaveLogSignal.Dispatch();
        }
    }
}