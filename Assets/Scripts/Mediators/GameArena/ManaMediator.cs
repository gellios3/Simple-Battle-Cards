using Models.Arena;
using Signals.GameArena;
using View.GameArena;

namespace Mediators.GameArena
{
    public class ManaMediator : TargetMediator<ManaView>
    {
        /// <summary>
        /// Show mana signal
        /// </summary>
        [Inject]
        public ShowManaSignal ShowManaSignal { get; set; }

        /// <summary>
        /// Init mana signal
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }

        /// <summary>
        /// On register mediator
        /// </summary>
        public override void OnRegister()
        {
            ShowManaSignal.AddListener(count =>
            {
//                if (BattleArena.ActiveSide == BattleSide.Player)
//                {
                View.InitManaView(count);
//                }
            });
        }
    }
}