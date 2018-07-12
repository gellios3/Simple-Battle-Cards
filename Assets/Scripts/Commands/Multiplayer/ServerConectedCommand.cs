using Models.Miltiplayer;
using strange.extensions.command.impl;
using Services.Multiplayer;
using UnityEngine;

namespace Commands.Multiplayer
{
    public class ServerConectedCommand : Command
    {
        /// <summary>
        /// Server connector service
        /// </summary>
        [Inject]
        public ServerConnectorService ServerConnectorService { get; set; } 
        
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
            NetworkPlayerService.PlayerStruct = new PlayerStruct
            {
                Id = Random.Range(0, 1000),
                Name = "test " + Random.Range(0, 100),
                IsConected = true
            };
            // Register player on server
            ServerConnectorService.Send(MsgStruct.SendPlayer, new RegisterPlayerMessage
            {
                Id = NetworkPlayerService.PlayerStruct.Id,
                Name = NetworkPlayerService.PlayerStruct.Name,
                IsConected = NetworkPlayerService.PlayerStruct.IsConected
            });
        }
    }
}