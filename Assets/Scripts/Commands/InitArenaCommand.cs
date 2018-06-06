using Models;
using Models.Arena;
using Models.ScriptableObjects;
using strange.extensions.command.impl;
using Signals.Arena;
using UniRx;
using UnityEngine;

namespace Commands
{
    public class InitArenaCommand : Command
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
        public ArenaGameManager ArenaGameManager { get; set; }

        /// <summary>
        /// Areana initialed signal
        /// </summary>
        [Inject]
        public ArenaInitializedSignal ArenaInitializedSignal { get; set; }

        /// <summary>
        /// Execute event load rooms list 
        /// </summary>
        public override void Execute()
        {
            // Load regular deck
            var deck = Resources.Load<Deck>("Objects/Decks/Regular");
            // init arena
            Arena.Init(deck, deck);
            Arena.YourPlayer.Name = "HUMAN";
            Arena.EnemyPlayer.Name = "CPU 1";
            // Init batle in your turn
            BattleArena.ActiveState = BattleState.YourTurn;
            ArenaGameManager.EmulateGame();

            Observable.Start(() => { Debug.Log("Logic"); }).ObserveOnMainThread()
                .Subscribe(res => { ArenaInitializedSignal.Dispatch(); });
        }
    }
}