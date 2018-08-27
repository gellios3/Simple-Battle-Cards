using Models.Multiplayer;
using strange.extensions.command.impl;
using Services.Multiplayer;
using Signals.multiplayer;
using UnityEngine;

namespace Commands.Multiplayer
{
    public class StartOnlineGameCommand : Command
    {
        [Inject] public string PlayerName { get; set; }

        /// <summary>
        /// Network player service
        /// </summary>
        [Inject]
        public NetworkPlayerService NetworkPlayerService { get; set; }

        /// <summary>
        /// Execute event add log
        /// </summary>
        public override void Execute()
        {
            Debug.Log("StartOnlineGameCommand " + PlayerName);

            NetworkPlayerService.NetworkLobbyPlayer = new NetworkLobbyPlayer
            {
                Id = Random.Range(0, 1000),
                Name = PlayerName
            };
        }
    }
}