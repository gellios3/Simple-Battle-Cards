using Signals.GameArena;
using View.GameArena;

namespace Mediators.GameArena
{
    public class AttackLineMediator : TargetMediator<AttackLineView>
    {
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

        /// <summary>
        /// On register mediator
        /// </summary>
        public override void OnRegister()
        {
            InitAttackLineSignal.AddListener(isActive =>
            {
                View.SetActive(isActive);
            });

            SetAttackLinePosSignal.AddListener(posStruct =>
            {
                View.SetLinePosition(posStruct.StartPos, posStruct.EndPos);
            });
        }
    }
}