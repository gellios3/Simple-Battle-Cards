using Models;
using Models.Arena;
using Signals.GameArena;
using Signals.GameArena.CardSignals;
using Signals.GameArena.TrateSignals;
using UnityEngine;
using View.DeckItems;

namespace Mediators.GameArena
{
    public class CardMediator : TargetMediator<CardView>
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
        /// Init mana signal
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }

        public override void OnRegister()
        {
            View.OnAddTrateToCard += view => { AddTrateToCardSignal.Dispatch(View, view); };

            View.OnStartDrag += view =>
            {
                if (view.Side != BattleArena.ActiveSide)
                {
                    view.IsDragable = false;
                }
                else
                {
                    view.IsDragable = view.Card.Status != BattleStatus.Sleep;
                }
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