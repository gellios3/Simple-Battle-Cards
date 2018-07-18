using UnityEngine.Networking;

namespace Models.Miltiplayer.Messages
{
    public class RegisterPlayerMessage : MessageBase
    {
        public int Id;
        public string Name;
    }
}