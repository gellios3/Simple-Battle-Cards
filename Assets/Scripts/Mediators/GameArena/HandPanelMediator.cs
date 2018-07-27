using Models.Arena;
using Signals;
using Signals.GameArena;
using Signals.GameArena.TrateSignals;
using UnityEngine;
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
        /// Init mana signal
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }    


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
           
        }

      
    }
}