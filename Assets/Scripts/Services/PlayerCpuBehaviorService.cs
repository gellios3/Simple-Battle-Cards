using System.Collections.Generic;
using Models;
using Models.Arena;
using Signals;

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
        /// Make Battle turn
        /// </summary>
        public void MakeBattleTurn()
        {
            // add all cards to battle arena

//            foreach (var battleItem in StateService.ActiveArenaPlayer.BattleHand.FindAll(item =>
//            {
//                var card = item as BattleCard;
//                return card != null;
//            }))
//            {
//                var item = (BattleCard) battleItem;
//                if (StateService.ActiveArenaPlayer.ManaPull <= 0) continue;
//                if (BattleArena.ActiveBattleTurnService.AddCardToArenaFromHand(item))
//                {
//                    item.Status = BattleStatus.Active;
//                }
//            }

            // add all trares to card
//            if (StateService.ActiveArenaPlayer.ArenaCards.Count > 0)
//            {
//                foreach (var battleItem in StateService.ActiveArenaPlayer.BattleHand.FindAll(item =>
//                {
//                    var trate = item as BattleTrate;
//                    return trate != null;
//                }))
//                {
//                    var trate = (BattleTrate) battleItem;
//                    if (StateService.ActiveArenaPlayer.ManaPull <= 0 || StateService.ActiveArenaPlayer.ManaPull < trate.Mana) continue;
//                    AddTrateToCard(trate);
//                }
//            }

         

            // Atack all emeny cards 
//            var enemyCards = GetEnemyActiveCards();
//            var activeCards = StateService.ActiveArenaPlayer.ArenaCards.FindAll(card => card.Status == BattleStatus.Active);
//            if (enemyCards.Count > 0 && activeCards.Count > 0)
//            {
//                foreach (var yourCard in activeCards)
//                {
//                    foreach (var enemyCard in enemyCards.FindAll(card => card.Status != BattleStatus.Dead))
//                    {
//                        if (yourCard.Status == BattleStatus.Active)
//                        {
////                            BattleArena.ActiveBattleTurnService.HitEnemyCard(yourCard, enemyCard);
//                        }
//                    }
//                }
//            }

            // End turn
//            BattleArena.EndTurn();
        }

//        /// <summary>
//        /// Add trate to card
//        /// </summary>
//        /// <param name="trate"></param>
//        /// <returns></returns>
//        private void AddTrateToCard(BattleTrate trate)
//        {
//            foreach (var arenaCard in StateService.ActiveArenaPlayer.ArenaCards)
//            {
//                if (trate.Status == BattleStatus.Active && arenaCard.Status != BattleStatus.Dead) continue;
//                // add trate to active cart and return true if them added
//                if (!BattleArena.ActiveBattleTurnService.AddTrateToActiveCard(arenaCard, trate)) continue;
//                trate.Status = BattleStatus.Active;
//                break;
//            }
//
//            if (trate.Status != BattleStatus.Active)
//            {
//                // @todo call not enough space
//                AddHistoryLogSignal.Dispatch(new[]
//                {
//                    "PLAYER '", StateService.ActiveArenaPlayer.Name, "' has ERROR! Add Trate '",
//                    trate.SourceTrate.name, "' to cart 'not enough space'"
//                }, LogType.Hand);
//            }
//        }

//        /// <summary>
//        /// Get emeny active cards
//        /// </summary>
//        /// <returns></returns>
//        private List<BattleCard> GetEnemyActiveCards()
//        {
//            return BattleArena.ActiveSide == BattleSide.Player
//                ? Arena.Opponent.ArenaCards
//                : Arena.Player.ArenaCards;
//        }
    }
}