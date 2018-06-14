using strange.extensions.command.impl;
using Services;
using UniRx;
using UnityEngine;

namespace Commands
{
    public class SaveGameCommand : Command
    {
        /// <summary>
        /// Load save sessipon data service
        /// </summary>
        [Inject]
        public LoadSaveGameService LoadSaveGameService { get; set; }

        /// <summary>
        /// Execute event init areana
        /// </summary>
        public override void Execute()
        {
            Observable.Start(() => { LoadSaveGameService.SaveGameSession("Assets/Resources/SaveState/save-log1.json"); })
                .ObserveOnMainThread()
                .Subscribe(res => { Debug.Log("Game Saved!"); });
        }
    }
}