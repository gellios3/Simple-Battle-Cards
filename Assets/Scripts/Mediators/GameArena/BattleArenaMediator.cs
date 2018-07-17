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
    public class BattleArenaMediator : TargetMediator<BattleArenaView>
    {
        /// <summary>
        /// Show card on battle arena signal
        /// </summary>
        [Inject]
        public ShowCardOnBattleArenaSignal ShowCardOnBattleArenaSignal { get; set; }

        /// <summary>
        /// Init mana signal
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }

        /// <summary>
        /// Battle
        /// </summary>
        [Inject]
        public ActivateBattleCards ActivateBattleCards { get; set; }

        /// <summary>
        /// Add history log signal
        /// </summary>
        [Inject]
        public AddHistoryLogSignal AddHistoryLogSignal { get; set; }

        /// <summary>
        /// Card Views
        /// </summary>
        private readonly List<CardView> _cardViews = new List<CardView>();

        /// <summary>
        /// On regisre mediator
        /// </summary>
        public override void OnRegister()
        {
            ShowCardOnBattleArenaSignal.AddListener(view =>
            {
                if (BattleArena.ActiveSide != View.Side) return;
                _cardViews.Add(view);
                View.AddCardViewToArena(view);
            });

            ActivateBattleCards.AddListener(() =>
            {
                if (BattleArena.ActiveSide != View.Side) return;

                var activePlayer = BattleArena.GetActivePlayer();
                // Set active all not dead areana cards 
                foreach (var cardView in _cardViews)
                {
                    if (cardView.Card.Status == BattleStatus.Active) continue;
                    cardView.Card.Status = BattleStatus.Active;
                    AddHistoryLogSignal.Dispatch(new[]
                    {
                        "PLAYER '", activePlayer.Name, "' Activate '",
                        cardView.Card.SourceCard.name, "' battle card!"
                    }, LogType.Battle);
                    cardView.Init(cardView.Card);
                }

                // remove all dead carts
                activePlayer.ArenaCards = activePlayer.ArenaCards.FindAll(
                    card => card.Status == BattleStatus.Active
                );
            });
        }
    }
}