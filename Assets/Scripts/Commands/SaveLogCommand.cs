using System;
using System.IO;
using Models.Arena;
using Newtonsoft.Json;
using strange.extensions.command.impl;
using UniRx;
using UnityEngine;

namespace Commands
{
    public class SaveLogCommand : Command
    {
        /// <summary>
        /// Battle
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }

        /// <summary>
        /// Execute event game finished
        /// </summary>
        public override void Execute()
        {
            Observable.Start(() =>
            {
                var dateTime = DateTime.Now.Millisecond.ToString();
                //open file stream
                using (var file = File.AppendText("Assets/Resources/Log/" + dateTime + "-log.json"))
                {
                    var serializer = new JsonSerializer();
                    //serialize object directly into file stream
                    serializer.Serialize(file, BattleArena.History);
                }
            }).ObserveOnMainThread().Subscribe(res =>
            {
                Debug.Log("Session Saved!");
            });
        }
    }
}