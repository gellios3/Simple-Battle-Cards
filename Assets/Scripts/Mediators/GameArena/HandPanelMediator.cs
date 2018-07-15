using Models.Arena;
using Signals.GameArena;
using View.GameArena;

namespace Mediators.GameArena
{
    public class HandPanelMediator : TargetMediator<HandPanelView>
    {
        /// <summary>
        /// Init card deck signal
        /// </summary>
        [Inject]
        public AddCardToHandViewSignal AddCardToHandViewSignal { get; set; }

        /// <summary>
        /// Init card deck signal
        /// </summary>
        [Inject]
        public AddTrateToHandViewSignal AddTrateToHandViewSignal { get; set; }

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
            AddCardToHandViewSignal.AddListener(card =>
            {
                if (BattleArena.ActiveSide == View.GetCurrentSide())
                {
                    View.AddCardToHand(card);
                }
            });

            AddTrateToHandViewSignal.AddListener(trate =>
            {
                if (BattleArena.ActiveSide == View.GetCurrentSide())
                {
                    View.AddTrateToHand(trate);
                }
            });
        }
    }
}