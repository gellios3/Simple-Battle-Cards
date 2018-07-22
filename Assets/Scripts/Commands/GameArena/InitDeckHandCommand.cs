using Models.Arena;
using strange.extensions.command.impl;
using Signals.GameArena;
using Signals.GameArena.CardSignals;
using Signals.GameArena.TrateSignals;
using UnityEngine;

namespace Commands.GameArena
{
    public class InitDeckHandCommand : Command
    {
        /// <summary>
        /// Battle
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }

        /// <summary>
        /// Add card from deck to hand signal
        /// </summary>
        [Inject]
        public AddCardToHandDeckSignal AddCardToHandDeckSignal { get; set; }

        /// <summary>
        /// Add trate from deck to hand signal
        /// </summary>
        [Inject]
        public AddTrateFromDeckToHandSignal AddTrateFromDeckToHandSignal { get; set; }

        /// <summary>
        /// Execute event init hand
        /// </summary>
        public override void Execute()
        {
            // Add card or trates to hand
            if (BattleArena.GetActivePlayer().CardBattlePull.Count > 0)
            {
                AddCardToHandDeckSignal.Dispatch();
                AddCardToHandDeckSignal.Dispatch();

//                for (var i = 1; i < BattleArena.CountOfCardsAddingToHand; i++)
//                {
//                    if (Random.value > 0.5f)
//                    {
//                        if (BattleArena.GetActivePlayer().CardBattlePull.Count <= 0) continue;
//                        AddCardToHandDeckSignal.Dispatch();
//                    }
//                    else
//                    {
//                        if (BattleArena.GetActivePlayer().TrateBattlePull.Count <= 0) continue;
//                        AddTrateFromDeckToHandSignal.Dispatch();
//                    }
//                }
            }
            else
            {
                for (var i = 0; i < BattleArena.CountOfCardsAddingToHand; i++)
                {
                    if (BattleArena.GetActivePlayer().TrateBattlePull.Count <= 0) continue;
                    AddTrateFromDeckToHandSignal.Dispatch();
                }
            }
        }
    }
}