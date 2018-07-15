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
        public InitCardDeckSignal InitCardDeckSignal { get; set; }

        /// <summary>
        /// Arena
        /// </summary>
        [Inject]
        public Arena Arena { get; set; }

        public override void OnRegister()
        {
            InitCardDeckSignal.AddListener(() =>
            {
                if (View.GetCurrentSide() == BattleSide.Player)
                {
                    View.SetCardDeckCount(Arena.Player.CardBattlePull.Count);
                }
                else if (View.GetCurrentSide() == BattleSide.Opponent)
                {
                    View.SetCardDeckCount(Arena.Opponent.CardBattlePull.Count);
                }
            });
        }
    }
}