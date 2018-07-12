using Signals.Arena;
using View;

namespace Mediators
{
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
            ArenaInitializedSignal.AddListener(() => { View.OnArenaInitialized(); });
        }
    }
}