using Models.Arena;
using Signals.GameArena;
using View.GameArena;

namespace Mediators.GameArena
{
    public class TurnPopupMediator : TargetMediator<TurnPopupView>
    {
        /// <summary>
        /// Show turn popup signal
        /// </summary>
        [Inject] public ShowTurnPopupSignal ShowTurnPopupSignal { get; set; }

        /// <summary>
        /// Init mana signal
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }

        public override void OnRegister()
        {
            ShowTurnPopupSignal.AddListener(() =>
            {
                View.ShowPopup(BattleArena.ActiveSide == BattleSide.Player ? "Your turn" : "Opponent turn");
            });
        }
    }
}