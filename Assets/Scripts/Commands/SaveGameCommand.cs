using strange.extensions.command.impl;
using Services;
using UniRx;
using UnityEngine;

namespace Commands
{
    public class SaveGameCommand : Command
    {
        [Inject] public SaveService SaveService { get; set; }

        /// <summary>
        /// Execute event init areana
        /// </summary>
        public override void Execute()
        {
            Observable.Start(() => { SaveService.SaveData(); }).ObserveOnMainThread()
                .Subscribe(res => { Debug.Log("Game Saved!"); });
        }
    }
}