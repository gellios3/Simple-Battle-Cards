using System.Collections.Generic;
using Models;
using Models.Arena;
using Signals;
using Signals.GameArena;
using Signals.GameArena.CardSignals;
using View.DeckItems;
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
        /// Battle player hand
        /// </summary>
        public List<CardView> CardDeck { get; set; } = new List<CardView>();

        public override void OnRegister()
        {
            View.OnAddViewToDeck += view => { CardDeck.Add(view); };

            InitCardDeckSignal.AddListener(() =>
            {
                if (View.Side == BattleSide.Player)
                {
                    View.SetCardDeckCount(Arena.Player.CardBattlePull.Count);
                }
                else if (View.Side == BattleSide.Opponent)
                {
                    View.SetCardDeckCount(Arena.Opponent.CardBattlePull.Count);
                }
            });

            AddCardToHandDeckSignal.AddListener(() =>
            {
                if (BattleArena.ActiveSide != View.Side) return;
                AddCardToHand();
                // Init card desk
                InitCardDeckSignal.Dispatch();
            });
        }

        /// <summary>
        /// Add trate to hand
        /// </summary>
        private void AddCardToHand()
        {
            if (CardDeck.Count < Arena.HandLimitCount)
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

            // Decreace Card pull
            BattleArena.GetActivePlayer().CardBattlePull.RemoveAt(0);
        }
    }
}