using System;
using System.Collections.Generic;
using Services;
using Signals;
using Random = UnityEngine.Random;

namespace Models.Arena
{
    [Serializable]
    public class BattleArena
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
        /// Batle turn
        /// </summary>
        [Inject]
        public BattleTurnService ActiveBattleTurnService { get; private set; }

        /// <summary>
        /// Active state
        /// </summary>
        public BattleState ActiveState { get; set; }

        /// <summary>
        /// Battle history
        /// </summary>
        public readonly List<HistoryTurn> History = new List<HistoryTurn>();

        /// <summary>
        /// Init history
        /// </summary>
        public void InitHistory()
        {
            StateService.InitActiveHistoryTurn();
            History.Add(StateService.ActiveHistotyTurn);
        }

        /// <summary>
        /// Init battle turn
        /// </summary>
        public void InitActiveTurn()
        {
            // Increase turn count
            StateService.IncreaseTurnCount();

            // @todo call init turn 
            AddHistoryLogSignal.Dispatch(new[] {"INIT '", StateService.TurnCount.ToString(), "' TURN!"}, LogType.Hand);

            // On 2 Turn add more carts 
            var addCartcount = Arena.CartToAddCount;
            if (StateService.TurnCount == 2)
            {
                addCartcount++;
            }

            // Init mana pull
            StateService.ActivePlayer.InitManaPull();

            // Add 3 item to hand
            if (StateService.ActivePlayer.CardBattlePull.Count > 0)
            {
                if (!AddCartToPlayerHand())
                {
                    // @todo call not enough space in hand
                }
            }

            for (var i = 1; i < addCartcount; i++)
            {
                if (Random.Range(0, 2) == 0)
                {
                    if (StateService.ActivePlayer.CardBattlePull.Count <= 0) continue;
                    if (!AddCartToPlayerHand())
                    {
                        // @todo call not enough space in hand
                    }
                }
                else
                {
                    if (StateService.ActivePlayer.TrateBattlePull.Count <= 0) continue;
                    if (!AddTrateToPlayerHand())
                    {
                        // @todo call not enough space in hand            
                    }
                }
            }
        }

        /// <summary>
        /// Fill Battle hand
        /// </summary>
        private bool AddCartToPlayerHand()
        {
            var status = true;

            if (StateService.ActivePlayer.BattleHand.Count < Arena.HandLimitCount)
            {
                StateService.ActivePlayer.BattleHand.Add(StateService.ActivePlayer.CardBattlePull[0]);

                var card = StateService.ActivePlayer.CardBattlePull[0];
                if (card != null)
                {
                    AddHistoryLogSignal.Dispatch(new[]
                    {
                        "PLAYER '", StateService.ActivePlayer.Name, "' Add '", card.SourceCard.name,
                        "' Card to Hand"
                    }, LogType.Hand);
                }
            }
            else
            {
                status = false;

                AddHistoryLogSignal.Dispatch(
                    new[] {"PLAYER '", StateService.ActivePlayer.Name, "' has add Card to Hand ERROR! "},
                    LogType.Hand);
            }

            StateService.ActivePlayer.CardBattlePull.RemoveAt(0);


            return status;
        }

        /// <summary>
        /// ФAdd trate to player hand
        /// </summary>
        /// <returns></returns>
        private bool AddTrateToPlayerHand()
        {
            var status = true;

            if (StateService.ActivePlayer.BattleHand.Count < Arena.HandLimitCount)
            {
                StateService.ActivePlayer.BattleHand.Add(StateService.ActivePlayer.TrateBattlePull[0]);

                var trate = StateService.ActivePlayer.TrateBattlePull[0];
                if (trate != null)
                {
                    AddHistoryLogSignal.Dispatch(new[]
                    {
                        "PLAYER '", StateService.ActivePlayer.Name, "' Add '", trate.SourceTrate.name,
                        "' Trate To Hand"
                    }, LogType.Hand);
                }
            }
            else
            {
                status = false;

                AddHistoryLogSignal.Dispatch(
                    new[] {"PLAYER '", StateService.ActivePlayer.Name, "' has add Trate to Hand ERROR! "},
                    LogType.Hand);
            }

            StateService.ActivePlayer.TrateBattlePull.RemoveAt(0);

            return status;
        }

        /// <summary>
        /// End turn
        /// </summary>
        public void EndTurn()
        {
            // Activate all cards and remove dead carts
            foreach (var arenaCard in StateService.ActivePlayer.ArenaCards)
            {
                if (arenaCard.Status != BattleStatus.Moving) continue;
                arenaCard.Status = BattleStatus.Active;
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "PLAYER '", StateService.ActivePlayer.Name, "' Activate Moving '", arenaCard.SourceCard.name,
                    "' battle card!"
                }, LogType.Battle);
            }

            // Set active all not dead areana cards 
            foreach (var card in StateService.ActivePlayer.ArenaCards)
            {
                if (card.Status != BattleStatus.Wait) continue;
                card.Status = BattleStatus.Active;
                // 
                AddHistoryLogSignal.Dispatch(new[]
                {
                    "PLAYER '", StateService.ActivePlayer.Name, "' Activate sleep '", card.SourceCard.name,
                    "' battle card!"
                }, LogType.Battle);
            }

            // remove all dead carts
            StateService.ActivePlayer.ArenaCards = StateService.ActivePlayer.ArenaCards.FindAll(
                card => card.Status == BattleStatus.Active
            );

            // Switch active state
            ActiveState = ActiveState == BattleState.YourTurn ? BattleState.EnemyTurn : BattleState.YourTurn;
        }

        /// <summary>
        /// Is game over
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool IsGameOver(Player player)
        {
            return player.CardBattlePull.Count == 0 &&
                   player.BattleHand.FindAll(item =>
                   {
                       var card = item as BattleCard;
                       return card != null;
                   }).Count == 0 &&
                   player.ArenaCards.FindAll(card => card.Status != BattleStatus.Dead).Count == 0;
        }
    }

    public enum BattleState
    {
        YourTurn,
        EnemyTurn
    }
}