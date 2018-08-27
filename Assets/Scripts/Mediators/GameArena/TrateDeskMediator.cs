using Models;
using Models.Arena;
using Signals;
using Signals.GameArena;
using Signals.GameArena.TrateSignals;
using View.GameArena;

namespace Mediators.GameArena
{
    public class TrateDeskMediator : TargetMediator<TrateDeskView>
    {
        /// <summary>
        /// Init card deck signal
        /// </summary>
        [Inject]
        public InitTrateDeckSignal InitTrateDeckSignal { get; set; }
       

        /// <summary>
        /// Add history log
        /// </summary>
        [Inject]
        public AddHistoryLogSignal AddHistoryLogSignal { get; set; }
        
        /// <summary>
        /// Init card deck signal
        /// </summary>
        [Inject]
        public AddTrateToHandDeckSignal AddTrateToHandDeckSignal { get; set; } 
        
        /// <summary>
        /// Init card deck signal
        /// </summary>
        [Inject]
        public InitTrateHandSignal InitTrateHandSignal { get; set; }
        
        /// <summary>
        /// Init mana signal
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }

        /// <summary>
        /// On register mediator
        /// </summary>
        public override void OnRegister()
        {
            InitTrateDeckSignal.AddListener(() =>
            {
                View.InitDeckCount();
            });
            
            AddTrateToHandDeckSignal.AddListener(() =>
            {
                if (BattleArena.ActiveSide != View.Side) return;
                AddTrateToHand();
            });
            

            InitTrateHandSignal.AddListener(() => { View.AddPullTratesToHand(); });
        }
        
        /// <summary>
        /// Add trate to hand
        /// </summary>
        private void AddTrateToHand()
        {
            if (BattleArena.HandCardsCount < Arena.HandLimitCount)
            {
                var trate = BattleArena.GetActivePlayer().TrateBattlePull[0];
                if (trate != null)
                {
                    // add trate to battle hand                
                    View.AddTrateToDeck(trate, BattleArena.ActiveSide);

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
        }
    }
}