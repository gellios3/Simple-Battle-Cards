using Signals;
using Signals.Arena;
using Signals.GameArena;
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
        public InitBattleTurnSignal InitBattleTurnSignal { get; set; }
        
        

        /// <summary>
        /// On register mediator
        /// </summary>
        public override void OnRegister()
        {
            InitBattleTurnSignal.AddListener(() => { View.OnArenaInitialized(); });
        }
    }
}