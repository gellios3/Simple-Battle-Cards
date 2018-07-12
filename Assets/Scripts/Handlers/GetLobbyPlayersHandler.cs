using Interfaces;
using Models.Miltiplayer;
using UnityEngine;
using UnityEngine.Networking;

namespace Handlers
{
    public class GetLobbyPlayersHandler : IServerMessageHandler
    {
        public short MessageType => MsgStruct.GetRegisteredPlayers;

        public void Handle(NetworkMessage msg)
        {
            Debug.Log("GetLobbyPlayersHandler");
            var lobbyPlayersMessage = msg.ReadMessage<LobbyPlayersMessage>();
            if (lobbyPlayersMessage != null)
            {
                Debug.Log(lobbyPlayersMessage.Players.Length);
//                UpdateRegularGameDataSignal.Dispatch(new BaseRegularGame
//                {
//                    CurrentPlayers = regularMsg.CurrentPlayers,
//                    Id = regularMsg.Id,
//                    MaxPlayers = regularMsg.MaxPlayers,
//                    Name = regularMsg.Name,
//                    Price = regularMsg.Price
//                });
            }
        }
    }
}