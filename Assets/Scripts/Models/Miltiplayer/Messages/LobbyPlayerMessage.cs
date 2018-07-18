using UnityEngine.Networking;

namespace Models.Miltiplayer.Messages
{
    public class LobbyPlayerMessage : MessageBase
    {
        public int Id;
        public string Name;
    }
}