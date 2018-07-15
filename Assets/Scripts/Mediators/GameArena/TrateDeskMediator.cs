using Models.Arena;
using Signals.GameArena;
using View.GameArena;

namespace Mediators.GameArena
{
    public class TrateDeskMediator : TargetMediator<TrateDeskView>
    {
        /// <summary>
        /// Init card deck signal
        /// </summary>
        [Inject]
        public InitTrateDeckSignal InitTrateDeckSignal { get; set; }

        /// <summary>
        /// Arena
        /// </summary>
        [Inject]
        public Arena Arena { get; set; }

        public override void OnRegister()
        {
            InitTrateDeckSignal.AddListener(() =>
            {
                if (View.GetCurrentSide() == BattleSide.Player)
                {
                    View.SetTrateDeckCount(Arena.Player.TrateBattlePull.Count);
                }
                else if (View.GetCurrentSide() == BattleSide.Opponent)
                {
                    View.SetTrateDeckCount(Arena.Opponent.TrateBattlePull.Count);
                }
            });
        }
    }
}