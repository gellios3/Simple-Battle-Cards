using Models.Arena;
using Signals.GameArena;
using View.GameArena;

namespace Mediators.GameArena
{
    public class CardDeckMediator : TargetMediator<CardDeckView>
    {
        /// <summary>
        /// Init card deck signal
        /// </summary>
        [Inject]
        public InitAllCardDecksSignal InitAllCardDecksSignal { get; set; }

        /// <summary>
        /// Arena
        /// </summary>
        [Inject]
        public Arena Arena { get; set; }

        public override void OnRegister()
        {
            InitAllCardDecksSignal.AddListener(() =>
            {
                if (View.GetCurrentSide() == BattleSide.Player)
                {
                    View.SetDeckCount(Arena.Player.CardBattlePull.Count);
                }
                else if (View.GetCurrentSide() == BattleSide.Opponent)
                {
                    View.SetDeckCount(Arena.Opponent.CardBattlePull.Count);
                }
            });
        }
    }
}