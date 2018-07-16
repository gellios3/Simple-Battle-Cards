using Models;
using Models.Arena;
using strange.extensions.command.impl;
using Signals;
using Signals.GameArena;
using Signals.GameArena.CardSignals;
using View.DeckItems;

namespace Commands.GameArena
{
    public class AddCatdToBattleArenaCommand : Command
    {
        /// <summary>
        /// Init card deck signal
        /// </summary>
        [Inject]
        public CardView CardView { get; set; }

        /// <summary>
        /// Battle
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }

        /// <summary>
        /// Battle
        /// </summary>
        [Inject]
        public ShowCardOnBattleArenaSignal ShowCardOnBattleArenaSignal { get; set; }

        /// <summary>
        /// Add history log signal
        /// </summary>
        [Inject]
        public AddHistoryLogSignal AddHistoryLogSignal { get; set; }

        /// <summary>
        /// Init mana signal
        /// </summary>
        [Inject]
        public InitManaSignal InitManaSignal { get; set; }


        /// <summary>
        /// Execute add card to battle arena
        /// </summary>
        public override void Execute()
        {
            if (BattleArena.GetActivePlayer().ManaPull == 0)
            {
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "Player '", BattleArena.GetActivePlayer().Name, "' Has ERROR! add card '",
                    CardView.Card.SourceCard.name, "' to battle 'not enough mana!'"
                }, LogType.Hand);
            }
            else
            {
                if (!BattleArena.ActiveBattleTurnService.AddCardToArenaFromHand(CardView.Card)) return;
                // Activate card
                CardView.Card.Status = BattleStatus.Active;
                // Show card on battle arena
                ShowCardOnBattleArenaSignal.Dispatch(CardView);
                // Init mana view
                InitManaSignal.Dispatch();
            }
        }
    }
}