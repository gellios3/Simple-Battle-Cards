using Models.Arena;
using Signals.GameArena;
using View.GameArena;

namespace Mediators.GameArena
{
    public class SideDecksMediator : TargetMediator<SideDecksView>
    {
        /// <summary>
        /// Init card deck signal
        /// </summary>
        [Inject]
        public InitAllDecksSignal InitAllDecksSignal { get; set; }

        /// <summary>
        /// Arena
        /// </summary>
        [Inject]
        public Arena Arena { get; set; }

        public override void OnRegister()
        {
            InitAllDecksSignal.AddListener(() =>
            {
                if (View.GetCurrentSide() == BattleSide.Player)
                {
                    View.SetCardDeckCount(Arena.Player.CardBattlePull.Count);
                    View.SetTrateDeckCount(Arena.Player.TrateBattlePull.Count);
                }
                else if (View.GetCurrentSide() == BattleSide.Opponent)
                {
                    View.SetCardDeckCount(Arena.Opponent.CardBattlePull.Count);
                    View.SetTrateDeckCount(Arena.Opponent.TrateBattlePull.Count);
                }
            });
        }
    }
}