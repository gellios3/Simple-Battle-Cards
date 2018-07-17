using Models;
using Models.Arena;
using strange.extensions.command.impl;
using Signals;
using Signals.GameArena;
using Signals.GameArena.CardSignals;
using View.DeckItems;

namespace Commands.GameArena.CardCommands
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
            var activePlayer = BattleArena.GetActivePlayer();
            if (activePlayer.ManaPull == 0)
            {
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "Player '", activePlayer.Name, "' Has ERROR! add card '",
                    CardView.Card.SourceCard.name, "' to battle 'not enough mana!'"
                }, LogType.Hand);
            }
            else
            {
                if (activePlayer.ArenaCards.Count >= Arena.ArenaCartCount ||
                    CardView.Card.Status != BattleStatus.Wait)
                {
                    AddHistoryLogSignal.Dispatch(new[]
                    {
                        "PLAYER '", activePlayer.Name, "' has ERROR! Add Cart '",
                        CardView.Card.SourceCard.name, "' to Arena 'not enough space'"
                    }, LogType.Hand);
                    return;
                }

                if (!activePlayer.LessManaPull(CardView.Card.Mana))
                {
                    AddHistoryLogSignal.Dispatch(new[]
                    {
                        "Player '", activePlayer.Name, "' Has ERROR! add card '", CardView.Card.SourceCard.name,
                        "' to battle 'not enough mana!'"
                    }, LogType.Hand);
                    return;
                }

                activePlayer.ArenaCards.Add(new BattleCard(CardView.Card.SourceCard));
                // add history battle log
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "Player '", activePlayer.Name, "' Add card '", CardView.Card.SourceCard.name, "' to battle!"
                }, LogType.Hand);

                // Activate card
                CardView.Card.Status = BattleStatus.Sleep;
                // Show card on battle arena
                ShowCardOnBattleArenaSignal.Dispatch(CardView);
                // Init mana view
                InitManaSignal.Dispatch();
            }
        }
    }
}