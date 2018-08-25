using Models.Arena;
using Signals;
using Signals.GameArena;
using Signals.GameArena.TrateSignals;
using UnityEngine;
using View.AbstractViews;
using View.GameArena;
using View.GameItems;
using LogType = Models.LogType;

namespace Mediators.GameArena
{
    public class HandPanelMediator : TargetMediator<HandPanelView>
    {
        /// <summary>
        /// Init card deck signal
        /// </summary>
        [Inject]
        public RefreshHandSignal RefreshHandSignal { get; set; }

        /// <summary>
        /// Show mana signal
        /// </summary>
        [Inject]
        public ShowManaSignal ShowManaSignal { get; set; }

        /// <summary>
        /// Show mana signal
        /// </summary>
        [Inject]
        public ShowEndTurnButtonSignal ShowEndTurnButtonSignal { get; set; }


        /// <summary>
        /// Init mana signal
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }


        /// <summary>
        /// On register mediator
        /// </summary>
        public override void OnRegister()
        {
            ShowManaSignal.AddListener(manaCount =>
            {
                if (BattleArena.ActiveSide != View.Side)
                    return;
                if (View.transform.childCount > 0)
                {
                    var hasCard = false;
                    foreach (Transform child in View.transform)
                    {
                        var view = child.GetComponent<HandItemView>();
                        if (view != null && manaCount >= view.Mana)
                        {
                            hasCard = true;
                        }
                    }

                    if (!hasCard)
                    {
                        ShowEndTurnButtonSignal.Dispatch();
                    }
                }
                else if (manaCount < Arena.ManaPullCount)
                {
                    ShowEndTurnButtonSignal.Dispatch();
                }
            });

            RefreshHandSignal.AddListener(() =>
            {
                if (BattleArena.ActiveSide != View.Side)
                    return;
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
        }
    }
}