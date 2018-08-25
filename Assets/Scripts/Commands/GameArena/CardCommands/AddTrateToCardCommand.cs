using Models;
using Models.Arena;
using strange.extensions.command.impl;
using Signals;
using Signals.GameArena;
using View.GameArena;
using View.GameItems;

namespace Commands.GameArena.CardCommands
{
    public class AddTrateToCardCommand : Command
    {
        [Inject] public TrateView TrateView { get; set; }

        [Inject] public BattleUnitView BattleUnitView { get; set; }

        /// <summary>
        /// Init mana signal
        /// </summary>
        [Inject]
        public InitManaSignal InitManaSignal { get; set; }

        /// <summary>
        /// Battle arena
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }

        /// <summary>
        /// Add history log signal
        /// </summary>
        [Inject]
        public AddHistoryLogSignal AddHistoryLogSignal { get; set; }

        /// <summary>
        /// Init attack line signal
        /// </summary>
        [Inject]
        public InitAttackLineSignal InitAttackLineSignal { get; set; }

        public override void Execute()
        {
            var trate = TrateView.Trate;
            var card = BattleUnitView.Card;
            var activePlayer = BattleArena.GetActivePlayer();
            if (activePlayer.ManaPull <= 0 ||
                activePlayer.ManaPull < trate.Mana)
            {
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "Player '", activePlayer.Name, "' Has ERROR! 'Add trate' '",
                    trate.SourceTrate.name,
                    "' to battle card '", card.SourceCard.name, "' 'not enough mana!'"
                }, LogType.Hand);
                BattleArena.ApplyTrate = null;
                InitAttackLineSignal.Dispatch(false);
                return;
            }

            if (BattleUnitView.TrateViews.Count >= BattleCard.MaxTratesCount)
            {
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "PLAYER '", activePlayer.Name, "' has ERROR! Add Trate '",
                    trate.SourceTrate.name, "' to cart 'not enough space'"
                }, LogType.Hand);
                BattleArena.ApplyTrate = null;
                InitAttackLineSignal.Dispatch(false);
                return;
            }

            if (activePlayer.LessManaPull(trate.Mana))
            {
                BattleUnitView.AddTrate(TrateView);
                // add history battle log
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "Player '", activePlayer.Name, "' Add trate '", trate.SourceTrate.name,
                    "' to battle card '", card.SourceCard.name, "'"
                }, LogType.Hand);
            }
            else
            {
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "Player '", activePlayer.Name, "' Has ERROR! 'Add trate' '",
                    trate.SourceTrate.name,
                    "' to battle card '", card.SourceCard.name, "' 'not enough mana!'"
                }, LogType.Hand);
                BattleArena.ApplyTrate = null;
                InitAttackLineSignal.Dispatch(false);
                return;
            }

            BattleArena.ApplyTrate = null;
            InitAttackLineSignal.Dispatch(false);
            // Init mana view
            InitManaSignal.Dispatch();
        }
    }
}