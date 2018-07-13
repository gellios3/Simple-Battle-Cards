using Interfaces;
using Models.Miltiplayer;
using Services.Multiplayer;
using Signals.multiplayer;
using UnityEngine;
using UnityEngine.Networking;

namespace Handlers
{
    public class RemoveLobbyPlayerHandler : IServerMessageHandler
    {
        /// <summary>
        /// Massage type
        /// </summary>
        public short MessageType => MsgStruct.RemoveLobbyPlayer;

        /// <summary>
        /// Network player service
        /// </summary>
        [Inject]
        public NetworkPlayerService NetworkPlayerService { get; set; }

        /// <summary>
        /// Show lobby players signal
        /// </summary>
        [Inject]
        public ShowLobbyPlayersSignal ShowLobbyPlayersSignal { get; set; }

        /// <summary>
        /// Remove from lobby handler
        /// </summary>
        /// <param name="msg"></param>
        public void Handle(NetworkMessage msg)
        {
            var lobbyPlayerMessage = msg.ReadMessage<PingPlayerMessage>();
            if (lobbyPlayerMessage == null) return;
            for (var i = 0; i < NetworkPlayerService.OnlinePlayers.Count; i++)
            {
                if (NetworkPlayerService.OnlinePlayers[i].Id == lobbyPlayerMessage.Id)
                {
                    NetworkPlayerService.OnlinePlayers.RemoveAt(i);
                }
            }

            ShowLobbyPlayersSignal.Dispatch();
        }
    }
}