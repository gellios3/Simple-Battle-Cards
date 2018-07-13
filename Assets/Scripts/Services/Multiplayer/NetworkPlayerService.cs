using System.Collections.Generic;
using Models.Miltiplayer;

namespace Services.Multiplayer
{
    public class NetworkPlayerService
    {
        public NetworkPlayer NetworkPlayer { get; set; }

        public List<NetworkPlayer> OnlinePlayers { get; set; }
    }
}