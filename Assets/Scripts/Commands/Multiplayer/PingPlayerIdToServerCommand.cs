using Models.Miltiplayer;
using Models.Miltiplayer.Messages;
using strange.extensions.command.impl;
using Services.Multiplayer;
using UnityEngine;

namespace Commands.Multiplayer
{
    public class PingPlayerIdToServerCommand : Command
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
        /// Execute conect to server
        /// </summary>
        public override void Execute()
        {
            Debug.Log("PingPlayerIdToServerCommand");
            ServerConnectorService.Send(MsgStruct.SendPlayerID, new PingPlayerMessage
            {
                Id = NetworkPlayerService.NetworkLobbyPlayer.Id
            });
        }
    }
}