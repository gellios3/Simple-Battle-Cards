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

    /// <summary>
    /// Room grid view mediator
    /// </summary>
    public class GameArenaMediator : TargetMediator<GameArenaView>
    {
        /// <summary>
        /// Arena initialized signal
        /// </summary>
        [Inject]
        public ArenaInitializedSignal ArenaInitializedSignal { get; set; }

        /// <summary>
        /// On register mediator
        /// </summary>
        public override void OnRegister()
        {
            ArenaInitializedSignal.AddListener(ArenaInitialized);
        }

        /// <summary>
        /// On arena Initialized
        /// </summary>
        private void ArenaInitialized()
        {
            View.OnArenaInitialized();
        }
    }
}