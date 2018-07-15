using Models.Arena;
using strange.extensions.command.impl;
using Signals.GameArena;
using UnityEngine;

namespace Commands.GameArena
{
    public class InitHandPanelCommand : Command
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
        public AddCardFromDeckToHandSignal AddCardFromDeckToHandSignal { get; set; }

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
                AddCardFromDeckToHandSignal.Dispatch();
            }

            for (var i = 1; i < BattleArena.CountOfCardsAddingToHand; i++)
            {
                if (Random.value > 0.5f)
                {
                    if (BattleArena.GetActivePlayer().CardBattlePull.Count <= 0) continue;
                    AddCardFromDeckToHandSignal.Dispatch();
                }
                else
                {
                    if (BattleArena.GetActivePlayer().TrateBattlePull.Count <= 0) continue;
                    AddTrateFromDeckToHandSignal.Dispatch();
                }
            }
        }
    }
}