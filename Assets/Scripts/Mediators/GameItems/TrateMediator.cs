using Models.Arena;
using Signals.GameArena;
using UnityEngine;
using View.GameItems;

namespace Mediators.GameItems
{
    public class TrateMediator : TargetMediator<TrateView>
    {
        /// <summary>
        /// Init mana signal
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }

        /// <summary>
        /// Battle
        /// </summary>
        [Inject]
        public InitAttackLineSignal InitAttackLineSignal { get; set; }

        /// <summary>
        /// Battle
        /// </summary>
        [Inject]
        public SetAttackLinePosSignal SetAttackLinePosSignal { get; set; }

        public override void OnRegister()
        {
            View.OnInitApply += () =>
            {
                var tempHasApplyed = !View.HasApplyed;
                View.HasApplyed = tempHasApplyed;
                BattleArena.ApplyTrate = tempHasApplyed ? View : null;
                Debug.Log("OnInitApply " + tempHasApplyed);
                InitAttackLineSignal.Dispatch(tempHasApplyed);
            };

            View.OnDrawAttackLine += posStruct => { SetAttackLinePosSignal.Dispatch(posStruct); };
//            View.OnStartDrag += view => { view.CanDraggable = view.Side == BattleArena.ActiveSide; };
        }
    }
}