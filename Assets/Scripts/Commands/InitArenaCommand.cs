using System;
using System.IO;
using Models.Arena;
using Models.ScriptableObjects;
using Newtonsoft.Json;
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
            // Init batle in your turn
            BattleArena.ActiveState = BattleState.YourTurn;
            // init arena
            Arena.Init(deck, deck);
            Arena.YourPlayer.Name = "HUMAN";
            Arena.EnemyPlayer.Name = "CPU 1";

            ArenaGameManager.EmulateGame();

            Observable.Start(() =>
            {
                var dateTime = DateTime.Now.Millisecond.ToString();
                //open file stream
                using (var file = File.AppendText("Assets/Resources/Log/"+dateTime+"-log.json"))
                {
                    var serializer = new JsonSerializer();
                    //serialize object directly into file stream
                    serializer.Serialize(file, BattleArena.History);
                }
            }).ObserveOnMainThread().Subscribe(res => { ArenaInitializedSignal.Dispatch(); });
        }
    }
}