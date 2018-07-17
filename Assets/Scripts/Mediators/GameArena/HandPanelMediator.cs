using System.Collections.Generic;
using Models.Arena;
using Signals;
using Signals.GameArena;
using Signals.GameArena.CardSignals;
using Signals.GameArena.TrateSignals;
using UnityEngine;
using View.DeckItems;
using View.GameArena;
using LogType = Models.LogType;

namespace Mediators.GameArena
{
    public class HandPanelMediator : TargetMediator<HandPanelView>
    {
        /// <summary>
        /// Init card deck signal
        /// </summary>
        [Inject]
        public AddTrateFromDeckToHandSignal AddTrateFromDeckToHandSignal { get; set; }

        /// <summary>
        /// Init card deck signal
        /// </summary>
        [Inject]
        public RefreshHandSignal RefreshHandSignal { get; set; }

        /// <summary>
        /// Init card deck signal
        /// </summary>
        [Inject]
        public AddCardFromDeckToHandSignal AddCardFromDeckToHandSignal { get; set; }

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
        /// Init trate deck signal
        /// </summary>
        [Inject]
        public InitTrateDeckSignal InitTrateDeckSignal { get; set; }

        /// <summary>
        /// Add history log
        /// </summary>
        [Inject]
        public AddHistoryLogSignal AddHistoryLogSignal { get; set; }

        /// <summary>
        /// Battle player hand
        /// </summary>
        public List<DraggableView> BattleHand { get; set; } = new List<DraggableView>();

        /// <summary>
        /// On register mediator
        /// </summary>
        public override void OnRegister()
        {
            View.OnAddViewToHand += view => { BattleHand.Add(view); };

            RefreshHandSignal.AddListener(() =>
            {
                Debug.Log("RefreshHandSignal");
                if (BattleArena.ActiveSide != View.Side) return;
                BattleHand.Clear();
                foreach (Transform child in View.transform)
                {
                    var view = child.GetComponent<DraggableView>();
                    BattleHand.Add(view);
                }
            });

            AddTrateFromDeckToHandSignal.AddListener(() =>
            {
                if (BattleArena.ActiveSide != View.Side) return;
                AddTrateToHand();
                // Init trate deck signal
                InitTrateDeckSignal.Dispatch();
            });

            AddCardFromDeckToHandSignal.AddListener(() =>
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
            if (BattleHand.Count < Arena.HandLimitCount)
            {
                var card = BattleArena.GetActivePlayer().CardBattlePull[0];
                if (card != null)
                {
                    // add card to battle hand
                    View.AddCardToHand(card, BattleArena.ActiveSide);

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
        }

        /// <summary>
        /// Add trate to hand
        /// </summary>
        private void AddTrateToHand()
        {
            if (BattleHand.Count < Arena.HandLimitCount)
            {
                var trate = BattleArena.GetActivePlayer().TrateBattlePull[0];
                if (trate != null)
                {
                    // add trate to battle hand                
                    View.AddTrateToHand(trate, BattleArena.ActiveSide);

                    AddHistoryLogSignal.Dispatch(new[]
                    {
                        "PLAYER '", BattleArena.GetActivePlayer().Name, "' Add '", trate.SourceTrate.name,
                        "' Trate To Hand"
                    }, LogType.Hand);
                }
            }
            else
            {
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "PLAYER '", BattleArena.GetActivePlayer().Name, "' has add Trate to Hand ERROR! "
                }, LogType.Error);
            }

            BattleArena.GetActivePlayer().TrateBattlePull.RemoveAt(0);
        }
    }
}