using UnityEngine.Networking;

namespace Models.Multiplayer.Messages
{
    public class LobbyPlayerMessage : MessageBase
    {
        public int Id;
        public string Name;
    }
}