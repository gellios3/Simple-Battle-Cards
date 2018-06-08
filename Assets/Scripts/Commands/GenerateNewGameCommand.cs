using Models.Arena;
using Models.ScriptableObjects;
using strange.extensions.command.impl;
using Services;
using Signals.Arena;
using UnityEngine;

namespace Commands
{
    public class GenerateNewGameCommand : Command
    {
        /// <summary>
        /// Arena
        /// </summary>
        [Inject]
        public Arena Arena { get; set; }

        /// <summary>
        /// Battle
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }

        /// <summary>
        ///  Arena game manager
        /// </summary>
        [Inject]
        public GenarateGameSessionService GenarateGameSessionService { get; set; }

        /// <summary>
        /// Areana initialed signal
        /// </summary>
        [Inject]
        public ArenaInitializedSignal ArenaInitializedSignal { get; set; }

        /// <summary>
        /// Execute event init areana
        /// </summary>
        public override void Execute()
        {
            // Load regular deck
            var deck = Resources.Load<Deck>("Objects/Decks/Regular");
            // Init batle in your turn
            BattleArena.ActiveState = BattleState.YourTurn;
            // init arena
            Arena.Init(deck, deck);
            Arena.YourPlayer.Name = "HUMAN";
            Arena.EnemyPlayer.Name = "CPU 1";
            // Emulate game session
            GenarateGameSessionService.EmulateGameSession();
            ArenaInitializedSignal.Dispatch();
        }
    }
}