using Models.Arena;
using Models.ScriptableObjects;
using strange.extensions.command.impl;
using Services.GameArena;
using Signals;
using Signals.GameArena;
using UnityEngine;

namespace Commands.GameArena
{
    public class InitNewGameCommand : Command
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
        /// Arena initialized signal
        /// </summary>
        [Inject]
        public GameStateService GameStateService { get; set; }

        /// <summary>
        /// Areana initialed signal
        /// </summary>
        [Inject]
        public InitBattleTurnSignal InitBattleTurnSignal { get; set; }

        /// <summary>
        /// Execute event init areana
        /// </summary>
        public override void Execute()
        {
            // Load regular deck
            var cartDeck = Resources.Load<CartDeck>("Objects/Decks/CartDeck");
            var trateDeck = Resources.Load<TrateDeck>("Objects/Decks/TrateDeck");
            // Init batle in your turn
            BattleArena.ActiveSide = BattleSide.Player;
            // init arena
            Arena.Init(cartDeck, trateDeck);
            // Init single game players
            if (GameStateService.GameType == GameType.Single)
            {
                Arena.Player.Name = GameStateService.PlayerName;
                Arena.Opponent.IsCpu = true;
                Arena.Opponent.Name = "CPU 1";
            }
            // Init battle turn
            InitBattleTurnSignal.Dispatch();
        }
    }
}