using System.Collections.Generic;
using Models;
using Models.Arena;
using Models.ScriptableObjects;
using Signals;
using Signals.Arena;

namespace Services
{
    public class PlayerCpuBehaviorService
    {
        /// <summary>
        /// Battle
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }

        /// <summary>
        /// Arena
        /// </summary>
        [Inject]
        public Arena Arena { get; set; }

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
        /// Init turn
        /// </summary>
        public void InitTurn()
        {
            // init turn history
            BattleArena.InitHistory();
            // init active turn
            BattleArena.InitActiveTurn();
        }

        /// <summary>
        /// Make Battle turn
        /// </summary>
        public void MakeBattleTurn()
        {
            // add all cards to battle arena
            foreach (var item in StateService.ActivePlayer.BattleHand)
            {
                var card = item as BattleCard;
                if (card == null) continue;
                BattleArena.ActiveBattleTurnService.AddActiveCardFromHand(card);
                card.Status = BattleStatus.Active;
            }

            // add all trares to card
            if (StateService.ActivePlayer.ArenaCards.Count > 0)
            {
                foreach (var item in StateService.ActivePlayer.BattleHand)
                {
                    var trate = item as BattleTrate;
                    if (trate == null) continue;
                    if (!AddTrateToCard(trate))
                    {
                        // @todo call not enough space
                        AddHistoryLogSignal.Dispatch(new[]
                        {
                            "PLAYER '", StateService.ActivePlayer.Name, "' has ERROR! Add Trate '",
                            trate.SourceTrate.name, "'"
                        }, LogType.Hand);
                    }
                }
            }

            // Remove activate trate and cards
            StateService.ActivePlayer.BattleHand =
                StateService.ActivePlayer.BattleHand.FindAll(item => item.Status != BattleStatus.Active);

            // Atack all emeny cards 
            var enemyCards = GetEnemyActiveCards();
            var activeCards = StateService.ActivePlayer.ArenaCards.FindAll(card => card.Status == BattleStatus.Active);
            if (enemyCards.Count > 0 && activeCards.Count > 0)
            {
                foreach (var yourCard in activeCards)
                {
                    foreach (var enemyCard in enemyCards.FindAll(card => card.Status != BattleStatus.Dead))
                    {
                        BattleArena.ActiveBattleTurnService.HitEnemyCard(yourCard, enemyCard);
                    }
                }
            }

            // End turn
            BattleArena.EndTurn();
        }

        private bool AddTrateToCard(BattleTrate trate)
        {
            
            foreach (var arenaCard in StateService.ActivePlayer.ArenaCards)
            {
                if (trate.Status == BattleStatus.Active && arenaCard.Status != BattleStatus.Dead) continue;
                // add trate to active cart and return true if them added
                if (!BattleArena.ActiveBattleTurnService.AddTrateToActiveCard(arenaCard, trate)) continue;
                trate.Status = BattleStatus.Active;
                break;
            }

            return trate.Status == BattleStatus.Active;
        }

        /// <summary>
        /// Get emeny active cards
        /// </summary>
        /// <returns></returns>
        private List<BattleCard> GetEnemyActiveCards()
        {
            return BattleArena.ActiveState == BattleState.YourTurn
                ? Arena.EnemyPlayer.ArenaCards
                : Arena.YourPlayer.ArenaCards;
        }
    }
}