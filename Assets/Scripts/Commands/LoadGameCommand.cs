using strange.extensions.command.impl;
using Services;
using UniRx;
using UnityEngine;

namespace Commands
{
    public class LoadGameCommand : Command
    {
        /// <summary>
        /// Load save sessipon data service
        /// </summary>
        [Inject]
        public LoadSaveGameService LoadSaveGameService { get; set; }

        /// <summary>
        /// Execute event load game
        /// </summary>
        public override void Execute()
        {
            Observable.Start(() => LoadSaveGameService.LoadGameSession("Assets/Resources/SaveState/save-log.json"))
                .ObserveOnMainThread()
                .Subscribe(res =>
                {
                    Debug.Log(res);
                    Debug.Log("Game Loaded!");
                });
        }
    }
}