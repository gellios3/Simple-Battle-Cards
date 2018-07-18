using Models;
using Models.Arena;
using strange.extensions.command.impl;
using Signals;
using Signals.GameArena;
using View.DeckItems;

namespace Commands.GameArena.CardCommands
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
        public BattleArena BattleArena { get; set; }

        /// <summary>
        /// Add history log signal
        /// </summary>
        [Inject]
        public AddHistoryLogSignal AddHistoryLogSignal { get; set; }

        public override void Execute()
        {
            var trate = TrateView.Trate;
            var card = CardView.Card;
            var activePlayer = BattleArena.GetActivePlayer();
            if (activePlayer.ManaPull <= 0 ||
                activePlayer.ManaPull < trate.Mana)
            {
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "Player '", activePlayer.Name, "' Has ERROR! 'Add trate' '",
                    trate.SourceTrate.name,
                    "' to battle card '", card.SourceCard.name, "' 'not enough mana!'"
                }, LogType.Hand);
                return;
            }

            if (CardView.TrateViews.Count >= BattleCard.MaxTratesCount)
            {
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "PLAYER '", activePlayer.Name, "' has ERROR! Add Trate '",
                    trate.SourceTrate.name, "' to cart 'not enough space'"
                }, LogType.Hand);
                return;
            }

            if (activePlayer.LessManaPull(trate.Mana))
            {
                CardView.AddTrate(TrateView);
                // add history battle log
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "Player '", activePlayer.Name, "' Add trate '", trate.SourceTrate.name,
                    "' to battle card '", card.SourceCard.name, "'"
                }, LogType.Hand);
            }
            else
            {
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "Player '", activePlayer.Name, "' Has ERROR! 'Add trate' '",
                    trate.SourceTrate.name,
                    "' to battle card '", card.SourceCard.name, "' 'not enough mana!'"
                }, LogType.Hand);
                return;
            }

            // Init mana view
            InitManaSignal.Dispatch();
        }
    }
}