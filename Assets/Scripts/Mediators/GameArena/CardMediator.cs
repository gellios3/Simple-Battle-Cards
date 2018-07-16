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
        public UpdateCardSignal UpdateCardSignal { get; set; }

        public override void OnRegister()
        {
            View.OnAddTrateToCard += view => { AddTrateToCardSignal.Dispatch(View, view); };

            UpdateCardSignal.AddListener(card => { View.Init(card); });
        }
    }
}