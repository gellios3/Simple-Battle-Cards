using System.Collections.Generic;
using Interfaces;
using Models.Miltiplayer;
using Services.Multiplayer;
using Signals.multiplayer;
using UnityEngine.Networking;
using NetworkPlayer = Models.Miltiplayer.NetworkPlayer;

namespace Handlers
{
    public class GetLobbyPlayersHandler : IServerMessageHandler
    {
        public short MessageType => MsgStruct.GetRegisteredPlayers;

        [Inject] public NetworkPlayerService NetworkPlayerService { get; set; }
        [Inject] public ShowPlayersListSignal ShowPlayersListSignal { get; set; }

        /// <summary>
        /// Handle
        /// </summary>
        /// <param name="msg"></param>
        public void Handle(NetworkMessage msg)
        {
            var lobbyPlayersMessage = msg.ReadMessage<LobbyPlayersMessage>();
            if (lobbyPlayersMessage == null) return;

            NetworkPlayerService.OnlinePlayers = new List<NetworkPlayer>();
            foreach (var item in lobbyPlayersMessage.NetworkPlayers)
            {
                NetworkPlayerService.OnlinePlayers.Add(new NetworkPlayer
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }

            ShowPlayersListSignal.Dispatch();
        }
    }
}