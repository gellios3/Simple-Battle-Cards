using Models;
using Models.Arena;
using Signals.GameArena;
using Signals.GameArena.CardSignals;
using Signals.GameArena.TrateSignals;
using UnityEngine;
using View.GameItems;

namespace Mediators.GameArena
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

            View.OnInitAttack += () =>
            {
                if (View.Side != BattleArena.ActiveSide)
                    return;
                var tempHasAttack = !View.HasAttack;
                View.HasAttack = tempHasAttack;
                BattleArena.AttackUnit = tempHasAttack ? View : null;
                InitAttackLineSignal.Dispatch(tempHasAttack);
            };

            View.OnTakeDamage += view =>
            {
                if (View.Card.Status != BattleStatus.Wait &&
                    view.Side == BattleArena.ActiveSide &&
                    view.Card.Status == BattleStatus.Active)
                {
                    TakeDamageToCardSignal.Dispatch(new DamageStruct
                    {
                        DamageCardView = view,
                        SourceCardView = View
                    });
                }
            };
        }
    }
}