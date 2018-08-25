using System.Collections.Generic;
using System.Linq;
using Models.Arena;
using Signals;
using Signals.GameArena;
using Signals.GameArena.CardSignals;
using UnityEngine;
using View.GameArena;
using View.GameItems;
using LogType = Models.LogType;

namespace Mediators.GameArena
{
    public class BattleArenaMediator : TargetMediator<BattleArenaView>
    {
        /// <summary>
        /// Init mana signal
        /// </summary>
        [Inject]
        public InitManaSignal InitManaSignal { get; set; }

        /// <summary>
        /// Refresh arena signal
        /// </summary>
        [Inject]
        public RefreshArenaSignal RefreshArenaSignal { get; set; }

        /// <summary>
        /// Init mana signal
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }

        /// <summary>
        /// Battle
        /// </summary>
        [Inject]
        public ActivateBattleCardsSignal ActivateBattleCardsSignal { get; set; }

        /// <summary>
        /// Add history log signal
        /// </summary>
        [Inject]
        public AddHistoryLogSignal AddHistoryLogSignal { get; set; }

        /// <summary>
        /// Card Views
        /// </summary>
        private List<BattleUnitView> _cardUnitViews = new List<BattleUnitView>();

        /// <summary>
        /// On regisre mediator
        /// </summary>
        public override void OnRegister()
        {
            View.OnAddCatdToBattleArena += AddCardToBattleArena;

            RefreshArenaSignal.AddListener(() =>
            {
                if (BattleArena.ActiveSide != View.Side) return;
                _cardUnitViews.Clear();
                foreach (Transform child in View.transform)
                {
                    var view = child.GetComponent<BattleUnitView>();
                    _cardUnitViews.Add(view);
                }

                BattleArena.ArenaCardsCount = _cardUnitViews.Count;
            });

            ActivateBattleCardsSignal.AddListener(() =>
            {
                if (BattleArena.ActiveSide != View.Side) return;
                ActivateBattleCards();
            });
        }

        /// <summary>
        /// Activate Battle cards
        /// </summary>
        private void ActivateBattleCards()
        {
            var activePlayer = BattleArena.GetActivePlayer();
            // Set active all not dead areana cards 
            foreach (var cardView in _cardUnitViews)
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
            _cardUnitViews = _cardUnitViews.FindAll(
                view => view.Card.Status == BattleStatus.Active
            );
            _cardUnitViews.ForEach(view => view.HasActive = true);
        }

        /// <summary>
        ///  Add card to battle arena
        /// </summary>
        /// <param name="view"></param>
        private void AddCardToBattleArena(CardView view)
        {
            var activePlayer = BattleArena.GetActivePlayer();
            if (activePlayer.ManaPull == 0)
            {
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "Player '", activePlayer.Name, "' Has ERROR! add card '",
                    view.Card.SourceCard.name, "' to battle 'not enough mana!'"
                }, LogType.Hand);
                return;
            }

            var battleCard = view.Card;
            if (_cardUnitViews.Count >= Arena.ArenaCartCount ||
                battleCard.Status != BattleStatus.Wait)
            {
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "PLAYER '", activePlayer.Name, "' has ERROR! Add Cart '",
                    battleCard.SourceCard.name, "' to Arena 'not enough space'"
                }, LogType.Hand);
                return;
            }

            if (!activePlayer.LessManaPull(battleCard.Mana))
            {
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "Player '", activePlayer.Name, "' Has ERROR! add card '", battleCard.SourceCard.name,
                    "' to battle 'not enough mana!'"
                }, LogType.Hand);
                return;
            }

            // add history battle log
            AddHistoryLogSignal.Dispatch(new[]
            {
                "Player '", activePlayer.Name, "' Add card '", battleCard.SourceCard.name, "' to battle!"
            }, LogType.Hand);
            // Create card unit on battle arena             
            var unitView = View.CreateCardUnit(battleCard);

            _cardUnitViews.Add(unitView);
            // remove card from hand
            view.DestroyView();
            // Init mana view
            InitManaSignal.Dispatch();
        }
    }
}