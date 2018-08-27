using Signals.GameArena;
using View.GameArena;

namespace Mediators.GameArena
{
    public class EndTurnMediator : TargetMediator<EndTurnView>
    {
        /// <summary>
        /// Show mana signal
        /// </summary>
        [Inject]
        public ShowEndTurnButtonSignal ShowEndTurnButtonSignal { get; set; }

        /// <summary>
        /// On register mediator
        /// </summary>
        public override void OnRegister()
        {
            ShowEndTurnButtonSignal.AddListener(() => { View.ShowEndTurnButton(); });
        }
    }
}