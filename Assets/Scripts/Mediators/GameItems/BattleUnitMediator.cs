using Models;
using Models.Arena;
using Signals.GameArena;
using Signals.GameArena.CardSignals;
using Signals.GameArena.TrateSignals;
using View.GameItems;

namespace Mediators.GameItems
{
    public class BattleUnitMediator : TargetMediator<BattleUnitView>
    {
        /// <summary>
        /// Add trate to card signal
        /// </summary>
        [Inject]
        public AddTrateToCardSignal AddTrateToCardSignal { get; set; }

        /// <summary>
        /// Battle
        /// </summary>
        [Inject]
        public TakeDamageToCardSignal TakeDamageToCardSignal { get; set; }

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
        /// Init mana signal
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }


        /// <summary>
        /// On register
        /// </summary>
        public override void OnRegister()
        {
            View.OnAddTrateToCard += view => { AddTrateToCardSignal.Dispatch(View, view); };

            View.OnDrawAttackLine += posStruct => { SetAttackLinePosSignal.Dispatch(posStruct); };

            View.AttackBtnView.OnInitTakeDamage += OnInitTakeDamage;
            View.AttackBtnView.OnTakeDamage += OnTakeDamage;
            View.AttackBtnView.OnInitAttack += OnInitAttack;
        }

        /// <summary>
        /// On init take damage
        /// </summary>
        /// <param name="hasEnterDamage"></param>
        private void OnInitTakeDamage(bool hasEnterDamage)
        {
            if (View.Side == BattleArena.ActiveSide || BattleArena.AttackUnit == null)
                return;
            View.AttackBtnView.HasEnterOponentUnit = hasEnterDamage;
        }

        /// <summary>
        /// On init attack
        /// </summary>
        private void OnInitAttack()
        {
            if (View.Side != BattleArena.ActiveSide)
                return;

            var tempHasAttack = !View.HasAttack;
            View.HasAttack = tempHasAttack;
            BattleArena.AttackUnit = tempHasAttack ? View : null;
            InitAttackLineSignal.Dispatch(tempHasAttack);
        }

        /// <summary>
        /// On take damage
        /// </summary>
        private void OnTakeDamage()
        {
            if (BattleArena.AttackUnit == null ||
                View.Card.Status == BattleStatus.Wait ||
                BattleArena.AttackUnit.Side != BattleArena.ActiveSide ||
                BattleArena.AttackUnit.Card.Status != BattleStatus.Active)
                return;
            // Refresh attack status
            View.AttackBtnView.HasEnterOponentUnit = false;
            // Call tack damage signal
            TakeDamageToCardSignal.Dispatch(new DamageStruct
            {
                DamageCardView = BattleArena.AttackUnit,
                SourceCardView = View
            });
        }
    }
}