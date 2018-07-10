using System.Collections.Generic;
using System.Linq;
using Models;
using Models.Arena;
using Models.State;

namespace Services
{
    public class StateService
    {
        /// <summary>
        /// Active player
        /// </summary>
        public Player ActivePlayer { get; private set; }

        /// <summary>
        /// Turn history
        /// </summary>
        public HistoryTurn ActiveHistotyTurn { get; private set; }

        public int TurnCount { get; private set; }

        /// <summary>
        /// Increase turn count
        /// </summary>
        public void IncreaseTurnCount()
        {
            TurnCount++;
        }

        /// <summary>
        /// Init active History turn
        /// </summary>
        public void InitActiveHistoryTurn()
        {
            ActiveHistotyTurn = new HistoryTurn();
        }

        /// <summary>
        /// Init active player
        /// </summary>
        /// <param name="player"></param>
        public void InitActivePlayer(Player player)
        {
            ActivePlayer = player;
        }

        /// <summary>
        /// Get state player
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public StatePlayer GetStatePlayer(Player player)
        {
            var statePlayer = new StatePlayer
            {
                Name = player.Name,
                BattleHand = GetStateItems(player.BattleHand),
                ArenaCards = GetStateCards(player.ArenaCards),
                BattlePull = GetStateItems(player.CardBattlePull),
                isActive = ActivePlayer.Name == player.Name
            };

            return statePlayer;
        }

        /// <summary>
        /// Get state hand
        /// </summary>
        /// <param name="battleHand"></param>
        /// <returns></returns>
        private List<StateItem> GetStateItems(IEnumerable<BattleItem> battleHand)
        {
            var stateHand = new List<StateItem>();
            foreach (var item in battleHand)
            {
                var card = item as BattleCard;
                if (card != null)
                {
                    stateHand.Add(new StateCard
                    {
                        Attack = card.Attack,
                        Defence = card.Defence,
                        Health = card.Health,
                        Id = card.SourceCard.Id,
                        isCard = true
                    });
                }

                var trate = item as BattleTrate;
                if (trate != null)
                {
                    stateHand.Add(new StateTrate
                    {
                        Defence = trate.Defence,
                        Health = trate.Health,
                        Id = trate.SourceTrate.Id,
                        isCard = false
                    });
                }
            }

            return stateHand;
        }

        /// <summary>
        /// Get state Cards
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        private List<StateCard> GetStateCards(IEnumerable<BattleCard> cards)
        {
            return cards.Select(card => new StateCard
                {
                    Attack = card.Attack,
                    Defence = card.Defence,
                    Health = card.Health,
                    Id = card.SourceCard.Id,
                    BattleTrates = GetStateTrates(card.BattleTrates),
                    isCard = true
                })
                .ToList();
        }

        /// <summary>
        /// Get state Cards
        /// </summary>
        /// <param name="trates"></param>
        /// <returns></returns>
        private List<StateTrate> GetStateTrates(IEnumerable<BattleTrate> trates)
        {
            return trates.Select(card => new StateTrate
                {
                    Defence = card.Defence,
                    Health = card.Health,
                    Id = card.SourceTrate.Id,
                    isCard = false
                })
                .ToList();
        }
    }
}