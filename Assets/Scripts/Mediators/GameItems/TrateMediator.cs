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
            View.OnInitApply += InitApplyTrate;
            View.OnDrawAttackLine += posStruct => { SetAttackLinePosSignal.Dispatch(posStruct); };
        }

        private void InitApplyTrate()
        {
            if (View.Side != BattleArena.ActiveSide)
                return;
            var tempHasApplyed = !View.HasApplyed;
            View.HasApplyed = tempHasApplyed;
            BattleArena.ApplyTrate = tempHasApplyed ? View : null;
            InitAttackLineSignal.Dispatch(tempHasApplyed);
        }
    }
}