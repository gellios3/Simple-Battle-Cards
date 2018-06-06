using strange.extensions.mediation.impl;
using Signals.Arena;
using UnityEngine;

namespace View.Arena
{
    public class GameArenaView : EventView
    {
        public void OnArenaInitialized()
        {
            Debug.Log("OnInitArena");
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
        [Inject] public ArenaInitializedSignal ArenaInitializedSignal { get; set; }

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