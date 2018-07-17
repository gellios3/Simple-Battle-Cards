using Models;
using Models.Arena;
using Signals;

namespace Services
{
    public class BattleTurnService
    {
        /// <summary>
        /// State service
        /// </summary>
        [Inject]
        public StateService StateService { get; set; }

        /// <summary>
        /// Add history log signal
        /// </summary>
        [Inject]
        public AddHistoryLogSignal AddHistoryLogSignal { get; set; }


        /// <summary>
        /// Add trate to actice card
        /// </summary>
        /// <param name="card"></param>
        /// <param name="trate"></param>
        public bool AddTrateToActiveCard(BattleCard card, BattleTrate trate)
        {
            if (StateService.ActiveArenaPlayer.ManaPull <= 0 ||
                StateService.ActiveArenaPlayer.ManaPull < trate.Mana)
            {
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "Player '", StateService.ActiveArenaPlayer.Name, "' Has ERROR! 'Add trate' '",
                    trate.SourceTrate.name,
                    "' to battle card '", card.SourceCard.name, "' 'not enough mana!'"
                }, LogType.Hand);
                return false;
            }

            if (card.BattleTrates.Count >= BattleCard.MaxTratesCount)
            {
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "PLAYER '", StateService.ActiveArenaPlayer.Name, "' has ERROR! Add Trate '",
                    trate.SourceTrate.name, "' to cart 'not enough space'"
                }, LogType.Hand);
                return false;
            }

            if (StateService.ActiveArenaPlayer.LessManaPull(trate.Mana))
            {
                card.AddTrate(new BattleTrate(trate.SourceTrate));
                // add history battle log
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "Player '", StateService.ActiveArenaPlayer.Name, "' Add trate '", trate.SourceTrate.name,
                    "' to battle card '", card.SourceCard.name, "'"
                }, LogType.Hand);
            }
            else
            {
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "Player '", StateService.ActiveArenaPlayer.Name, "' Has ERROR! 'Add trate' '",
                    trate.SourceTrate.name,
                    "' to battle card '", card.SourceCard.name, "' 'not enough mana!'"
                }, LogType.Hand);
                return false;
            }

            return true;
        }
    }
}