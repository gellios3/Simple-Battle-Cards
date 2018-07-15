using Models;
using Models.Arena;
using strange.extensions.command.impl;
using Signals;

namespace Commands.GameArena
{
    public class AddTrateFromDeckToHandCommand : Command
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

        public override void Execute()
        {
            if (BattleArena.GetActivePlayer().BattleHand.Count < Arena.HandLimitCount)
            {
                BattleArena.GetActivePlayer().BattleHand.Add(BattleArena.GetActivePlayer().TrateBattlePull[0]);

                var trate = BattleArena.GetActivePlayer().TrateBattlePull[0];
                if (trate != null)
                {
                    AddHistoryLogSignal.Dispatch(new[]
                    {
                        "PLAYER '", BattleArena.GetActivePlayer().Name, "' Add '", trate.SourceTrate.name,
                        "' Trate To Hand"
                    }, LogType.Hand);
                }
            }
            else
            {
                //@todo call limit hand count
                AddHistoryLogSignal.Dispatch(
                    new[] {"PLAYER '", BattleArena.GetActivePlayer().Name, "' has add Trate to Hand ERROR! "},
                    LogType.Hand);
            }

            BattleArena.GetActivePlayer().TrateBattlePull.RemoveAt(0);
            //@todo call Decreace Card pull
        }
    }
}