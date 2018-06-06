using System.Collections.Generic;
using Models.Arena;

namespace Models
{
    public class PlayerCpuBehavior
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
        public Arena.Arena Arena { get; set; }

        /// <summary>
        /// Active player
        /// </summary>
        public Player ActivePlayer { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        public void Init(Player player)
        {
            ActivePlayer = player;
//            Debug.Log("Init Behavior for " + player.Name);
        }

        /// <summary>
        /// Init turn
        /// </summary>
        public void InitTurn()
        {
            // init active turn
            BattleArena.InitActiveTurn(ActivePlayer);
            // init battle turn 
            BattleArena.InitActiveBattleTurn(ActivePlayer);
//            Debug.Log("InitTurn for " + ActivePlayer.Name);
        }

        /// <summary>
        /// Make Battle turn
        /// </summary>
        public void MakeBattleTurn()
        {
            // add all cards to battle arena
            foreach (var item in ActivePlayer.BattleHand)
            {
                var card = item as BattleCard;
                if (card == null) continue;
                BattleArena.ActiveBattleTurn.AddActiveCardFromHand(card);
                card.Status = CardStatus.Active;
            }          

            // add all trares to card
            if (ActivePlayer.ArenaCards.Count > 0)
            {
                foreach (var item in ActivePlayer.BattleHand)
                {
                    var trate = item as BattleTrate;
                    if (trate == null) continue;
                    BattleArena.ActiveBattleTurn.AddTrateToActiveCard(ActivePlayer.ArenaCards[0], trate);
                    trate.isUsed = true;
                }
            }
            
            // remove activate trate and cards
            ActivePlayer.BattleHand = ActivePlayer.BattleHand.FindAll(item =>
            {
                var card = item as BattleCard;
                var trate = item as BattleTrate;
                return card != null && card.Status != CardStatus.Active || trate != null && !trate.isUsed;
            });

            // Atack all emeny cards 
            var enemyCards = GetEnemyActiveCards();
            var activeCards = ActivePlayer.ArenaCards.FindAll(card => card.Status == CardStatus.Active);
            if (enemyCards.Count > 0 && activeCards.Count > 0)
            {
                foreach (var yourCard in activeCards)
                {
                    foreach (var enemyCard in enemyCards.FindAll(card => card.Status != CardStatus.Dead))
                    {
                        BattleArena.ActiveBattleTurn.HitEnemyCard(yourCard, enemyCard);
                    }
                }
            }

            // End turn
            BattleArena.EndTurn(ActivePlayer);
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