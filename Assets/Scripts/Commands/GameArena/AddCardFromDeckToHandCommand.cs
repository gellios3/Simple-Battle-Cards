using Models;
using Models.Arena;
using strange.extensions.command.impl;
using Signals;
using Signals.GameArena;
using Signals.GameArena.CardSignals;

namespace Commands.GameArena
{
    public class AddCardFromDeckToHandCommand : Command
    {
        /// <summary>
        /// Battle
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }

        /// <summary>
        /// Add history log
        /// </summary>
        [Inject]
        public AddHistoryLogSignal AddHistoryLogSignal { get; set; }

        /// <summary>
        /// Init card deck signal
        /// </summary>
        [Inject]
        public InitCardDeckSignal InitCardDeckSignal { get; set; }
        
        /// <summary>
        /// Init card deck signal
        /// </summary>
        [Inject]
        public AddCardToHandViewSignal AddCardToHandViewSignal { get; set; }

        /// <summary>
        /// Execute add card to hand log
        /// </summary>
        public override void Execute()
        {
            if (BattleArena.GetActivePlayer().BattleHand.Count < Arena.HandLimitCount)
            {
                var card = BattleArena.GetActivePlayer().CardBattlePull[0];
                if (card != null)
                {
                    // add card to battle hand
                    BattleArena.GetActivePlayer().BattleHand.Add(card);                   
                    AddCardToHandViewSignal.Dispatch(card);

                    AddHistoryLogSignal.Dispatch(new[]
                    {
                        "PLAYER '", BattleArena.GetActivePlayer().Name, "' Add '", card.SourceCard.name,
                        "' Card to Hand"
                    }, LogType.Hand);
                }
            }
            else
            {
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "PLAYER '", BattleArena.GetActivePlayer().Name, "' has add Card to Hand ERROR! "
                }, LogType.Error);
            }

            // Decreace Card pull
            BattleArena.GetActivePlayer().CardBattlePull.RemoveAt(0);

            // Init card desk
            InitCardDeckSignal.Dispatch();
        }
    }
}