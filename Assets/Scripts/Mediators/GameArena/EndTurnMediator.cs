using Signals.GameArena;
using View.GameArena;

namespace Mediators.GameArena
{
    public class EndTurnMediator : TargetMediator<EndTurnView>
    {
        [Inject] public EndTurnSignal EndTurnSignal { get; set; }

        public override void OnRegister()
        {
            View.EndTurnButton.onClick.AddListener(() => { EndTurnSignal.Dispatch(); });
        }
    }
}