using Models.Arena;
using Signals;
using Signals.GameArena;
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
        /// On register mediator
        /// </summary>
        public override void OnRegister()
        {
            RefreshHandSignal.AddListener(() =>
            {
                if (BattleArena.ActiveSide != View.Side) return;
                var count = 0;
                foreach (Transform child in View.transform)
                {
                    var view = child.GetComponent<CardView>();
                    if (view != null)
                    {
                        count++;
                    }
                }

                BattleArena.HandCount = View.transform.childCount;
                BattleArena.HandCardsCount = count;
            });

            AddTrateFromDeckToHandSignal.AddListener(() =>
            {
                if (BattleArena.ActiveSide != View.Side) return;
                AddTrateToHand();
                // Init trate deck signal
                InitTrateDeckSignal.Dispatch();
            });
        }

        /// <summary>
        /// Add trate to hand
        /// </summary>
        private void AddTrateToHand()
        {
            if (BattleArena.HandCardsCount < Arena.HandLimitCount)
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