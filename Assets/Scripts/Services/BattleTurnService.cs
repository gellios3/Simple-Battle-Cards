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
        /// Add active card from hand
        /// </summary>
        /// <param name="card"></param>
        public bool AddCardToArenaFromHand(BattleCard card)
        {
            if (StateService.ActiveArenaPlayer.ArenaCards.Count >= Arena.ArenaCartCount ||
                card.Status != BattleStatus.Wait)
            {
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "PLAYER '", StateService.ActiveArenaPlayer.Name, "' has ERROR! Add Cart '",
                    card.SourceCard.name, "' to Arena 'not enough space'"
                }, LogType.Hand);
                return false;
            }

            if (!StateService.ActiveArenaPlayer.LessManaPull(card.Mana))
            {
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "Player '", StateService.ActiveArenaPlayer.Name, "' Has ERROR! add card '", card.SourceCard.name,
                    "' to battle 'not enough mana!'"
                }, LogType.Hand);
                return false;
            }

            StateService.ActiveArenaPlayer.ArenaCards.Add(new BattleCard(card.SourceCard));
            // add history battle log
            AddHistoryLogSignal.Dispatch(new[]
            {
                "Player '", StateService.ActiveArenaPlayer.Name, "' Add card '", card.SourceCard.name, "' to battle!"
            }, LogType.Hand);

            return true;
        }

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

        /// <summary>
        /// Hit Enemy Card
        /// </summary>
        /// <param name="yourCard"></param>
        /// <param name="enemyCard"></param>
        public void HitEnemyCard(BattleCard yourCard, BattleCard enemyCard)
        {
            if (enemyCard.TakeDamage(yourCard))
            {
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "Player '", StateService.ActiveArenaPlayer.Name, "' Use Card '", yourCard.SourceCard.name,
                    "' hit CRITICAL ememy Card '", enemyCard.SourceCard.name, "'"
                }, LogType.Battle);
            }
            else
            {
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "Player '", StateService.ActiveArenaPlayer.Name, "' Use Card '", yourCard.SourceCard.name,
                    "' hit ememy Card '", enemyCard.SourceCard.name, "' take damage '", yourCard.Attack.ToString(), "'"
                }, LogType.Battle);
            }

            // Emeny cart return attack
            yourCard.TakeDamage(enemyCard, false);
            AddHistoryLogSignal.Dispatch(new[]
            {
                "Enemy Card '", enemyCard.SourceCard.name,
                "' return  damage '", enemyCard.Attack.ToString(), "' to '", yourCard.SourceCard.name, "'"
            }, LogType.Battle);


            if (yourCard.Status == BattleStatus.Dead)
            {
                AddHistoryLogSignal.Dispatch(new[] {"Player Card '", yourCard.SourceCard.name, "' has dead!"},
                    LogType.Battle);
            }
            else
            {
                yourCard.Status = BattleStatus.Moving;
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "Player card '", yourCard.SourceCard.name, "' has '", yourCard.Health.ToString(),
                    "' Health and '", yourCard.Defence.ToString(), "' Defence"
                }, LogType.Battle);
            }

            if (enemyCard.Status == BattleStatus.Dead)
            {
                AddHistoryLogSignal.Dispatch(new[] {"Enemy Card '", enemyCard.SourceCard.name, "' has dead!"},
                    LogType.Battle);
            }
            else
            {
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "Enemy card '", enemyCard.SourceCard.name, "' has '", enemyCard.Health.ToString(),
                    "' Health and '", enemyCard.Defence.ToString(), "' Defence"
                }, LogType.Battle);
            }
        }
    }
}