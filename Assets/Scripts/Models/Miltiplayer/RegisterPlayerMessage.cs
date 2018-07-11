using UnityEngine.Networking;

namespace Models.Miltiplayer
{
    public class RegisterPlayerMessage : MessageBase
    {
        public int Id;
        public string Name;
        public bool IsConected;
    }
}