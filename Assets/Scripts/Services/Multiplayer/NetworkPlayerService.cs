using System.Collections.Generic;
using Models.Multiplayer;

namespace Services.Multiplayer
{
    public class NetworkPlayerService
    {
        public NetworkLobbyPlayer NetworkLobbyPlayer { get; set; }

        public List<NetworkLobbyPlayer> OnlinePlayers { get; } = new List<NetworkLobbyPlayer>();
    }
}