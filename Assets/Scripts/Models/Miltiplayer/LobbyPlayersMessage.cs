using UnityEngine.Networking;

namespace Models.Miltiplayer
{
    public class LobbyPlayersMessage : MessageBase
    {
        public PlayerStruct[] Players;
    }
}