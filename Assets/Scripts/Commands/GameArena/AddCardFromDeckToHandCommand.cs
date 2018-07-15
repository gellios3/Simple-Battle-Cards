using Models;
using Models.Arena;
using strange.extensions.command.impl;
using Signals;

namespace Commands.GameArena
{
    public class AddCardFromDeckToHandCommand : Command
    {
        /// <summary>
        /// Battle
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }

        /// <summary>
        /// Add history log
        /// </summary>
        [Inject]
        public AddHistoryLogSignal AddHistoryLogSignal { get; set; }

        /// <summary>
        /// Execute add card to hand log
        /// </summary>
        public override void Execute()
        {
            if (BattleArena.GetActivePlayer().BattleHand.Count < Arena.HandLimitCount)
            {
                BattleArena.GetActivePlayer().BattleHand.Add(BattleArena.GetActivePlayer().CardBattlePull[0]);

                var card = BattleArena.GetActivePlayer().CardBattlePull[0];
                if (card != null)
                {
                    AddHistoryLogSignal.Dispatch(new[]
                    {
                        "PLAYER '", BattleArena.GetActivePlayer().Name, "' Add '", card.SourceCard.name,
                        "' Card to Hand"
                    }, LogType.Hand);
                }
            }
            else
            {
                //@todo call limit hand count
                AddHistoryLogSignal.Dispatch(
                    new[] {"PLAYER '", BattleArena.GetActivePlayer().Name, "' has add Card to Hand ERROR! "},
                    LogType.Hand);
            }

            // Decreace Card pull
            BattleArena.GetActivePlayer().CardBattlePull.RemoveAt(0);
            
            //@todo call Decreace Card pull
        }
    }
}