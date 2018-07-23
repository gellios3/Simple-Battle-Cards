using Models;
using Models.Arena;
using Signals;
using Signals.GameArena;
using Signals.GameArena.CardSignals;
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
        /// Init mana signal
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }

        /// <summary>
        /// Init card deck signal
        /// </summary>
        [Inject]
        public AddCardToHandDeckSignal AddCardToHandDeckSignal { get; set; }

        /// <summary>
        /// Add history log
        /// </summary>
        [Inject]
        public AddHistoryLogSignal AddHistoryLogSignal { get; set; }

        /// <summary>
        /// Init hand signal
        /// </summary>
        [Inject]
        public InitHandSignal InitHandSignal { get; set; }

        public override void OnRegister()
        {            
            InitCardDeckSignal.AddListener(() => { View.InitCardDeckCount(); });

            AddCardToHandDeckSignal.AddListener(() =>
            {
                if (BattleArena.ActiveSide != View.Side) return;
                AddCardToHand();
            });

            InitHandSignal.AddListener(() => { View.AddPullCardsToHand(); });
        }

        /// <summary>
        /// Add card to hand
        /// </summary>
        private void AddCardToHand()
        {
            if (BattleArena.HandCount < Arena.HandLimitCount)
            {
                var card = BattleArena.GetActivePlayer().CardBattlePull[0];
                if (card != null)
                {
                    // add card to battle hand
                    View.AddCardToDeck(card, BattleArena.ActiveSide);

                    AddHistoryLogSignal.Dispatch(new[]
                    {
                        "PLAYER '", BattleArena.GetActivePlayer().Name, "' Add '", card.SourceCard.name,
                        "' Card to Deck Hand"
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

            // Remove card from pull
            BattleArena.GetActivePlayer().CardBattlePull.RemoveAt(0);
        }
    }
}