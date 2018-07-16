using Models;
using Models.Arena;
using strange.extensions.command.impl;
using Signals;
using Signals.GameArena;
using Signals.GameArena.CardSignals;
using View.DeckItems;

namespace Commands.GameArena
{
    public class AddTrateToCardCommand : Command
    {
        [Inject] public TrateView TrateView { get; set; }

        [Inject] public CardView CardView { get; set; }
        
        /// <summary>
        /// Init mana signal
        /// </summary>
        [Inject]
        public InitManaSignal InitManaSignal { get; set; }
        
        /// <summary>
        /// Battle
        /// </summary>
        [Inject]
        public UpdateCardSignal UpdateCardSignal { get; set; }

        /// <summary>
        /// Battle
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }

        public override void Execute()
        {
            // add trate to active cart and return true if them added
            if (!BattleArena.ActiveBattleTurnService.AddTrateToActiveCard(CardView.Card,  TrateView.Trate)) return;
            // Init mana view
            InitManaSignal.Dispatch();
            // Show card on battle arena
            UpdateCardSignal.Dispatch(CardView.Card);
            TrateView.DestroyView();
        }
    }
}