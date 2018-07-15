using Models;
using Models.Arena;
using strange.extensions.command.impl;
using Signals;
using Signals.GameArena;

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
        /// Init trate deck signal
        /// </summary>
        [Inject]
        public InitTrateDeckSignal InitTrateDeckSignal { get; set; }

        /// <summary>
        /// Add history log
        /// </summary>
        [Inject]
        public AddHistoryLogSignal AddHistoryLogSignal { get; set; }

        public override void Execute()
        {
            if (BattleArena.GetActivePlayer().BattleHand.Count < Arena.HandLimitCount)
            {
                var trate = BattleArena.GetActivePlayer().TrateBattlePull[0];
                if (trate != null)
                {
                    BattleArena.GetActivePlayer().BattleHand.Add(trate);

                    // @todo Add trate to hand
                    AddHistoryLogSignal.Dispatch(new[]
                    {
                        "PLAYER '", BattleArena.GetActivePlayer().Name, "' Add '", trate.SourceTrate.name,
                        "' Trate To Hand"
                    }, LogType.Hand);
                }
            }
            else
            {
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "PLAYER '", BattleArena.GetActivePlayer().Name, "' has add Trate to Hand ERROR! "
                }, LogType.Error);
            }

            BattleArena.GetActivePlayer().TrateBattlePull.RemoveAt(0);

            // Init trate deck signal
            InitTrateDeckSignal.Dispatch();
        }
    }
}