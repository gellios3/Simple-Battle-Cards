using Models.Arena;
using Signals.GameArena;
using Signals.GameArena.CardSignals;
using View.GameArena;

namespace Mediators.GameArena
{
    public class BattleArenaMediator : TargetMediator<BattleArenaView>
    {
        /// <summary>
        /// Show card on battle arena signal
        /// </summary>
        [Inject]
        public ShowCardOnBattleArenaSignal ShowCardOnBattleArenaSignal { get; set; }

        /// <summary>
        /// Init mana signal
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }

        /// <summary>
        /// On regisre mediator
        /// </summary>
        public override void OnRegister()
        {
            ShowCardOnBattleArenaSignal.AddListener(view =>
            {
                if (BattleArena.ActiveSide == View.Side)
                {
                    View.AddCardViewToArena(view);
                }
            });
        }
    }
}