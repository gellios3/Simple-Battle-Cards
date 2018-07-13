using Interfaces;
using Models.Miltiplayer;
using Services.Multiplayer;
using Signals.multiplayer;
using UnityEngine.Networking;
using NetworkPlayer = Models.Miltiplayer.NetworkPlayer;

namespace Handlers
{
    public class GetLobbyPlayerHandler : IServerMessageHandler
    {
        public short MessageType => MsgStruct.SendLobbyPlayer;

        [Inject] public NetworkPlayerService NetworkPlayerService { get; set; }
        [Inject] public ShowLobbyPlayersSignal ShowLobbyPlayersSignal { get; set; }

        /// <summary>
        /// Handle
        /// </summary>
        /// <param name="msg"></param>
        public void Handle(NetworkMessage msg)
        {
            var lobbyPlayerMessage = msg.ReadMessage<LobbyPlayerMessage>();
            if (lobbyPlayerMessage == null) return;

            NetworkPlayerService.OnlinePlayers.Add(new NetworkPlayer
            {
                Id = lobbyPlayerMessage.Id,
                Name = lobbyPlayerMessage.Name
            });
            ShowLobbyPlayersSignal.Dispatch();
        }
    }
}