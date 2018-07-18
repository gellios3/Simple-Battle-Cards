using System;
using System.Collections.Generic;
using System.IO;
using Models.Arena;
using Models.State;
using strange.extensions.command.impl;
using Services;
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
        /// Json Work Service
        /// </summary>
        [Inject]
        public JsonWorkService JsonWorkService { get; set; }

        /// <summary>
        /// Execute event game finished
        /// </summary>
        public override void Execute()
        {
            // create state history log
            var stateHistory = new List<StateHistoryTurn>();
            foreach (var histotyTurn in BattleArena.History)
            {
                stateHistory.Add(new StateHistoryTurn
                {
                    BattleLog = histotyTurn.BattleLog.ToArray(),
                    HandLog = histotyTurn.HandLog.ToArray()
                });
            }

            var json = JsonWorkService.SerializeToJson(stateHistory);
            Observable.Start(() =>
            {
                var dateTime = DateTime.Now.Millisecond.ToString();

                using (var fs = new FileStream("Assets/Resources/Log/" + dateTime + "-log.json", FileMode.Create))
                {
                    using (var writer = new StreamWriter(fs))
                    {
                        writer.Write(json);
                    }
                }
            }).ObserveOnMainThread().Subscribe(res => { Debug.Log("Session Saved!"); });
        }
    }
}