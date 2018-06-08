using UnityEngine;

namespace Models.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Trate", menuName = "Trate")]
    public class Trate : ScriptableObject
    {
        public string Id;
        public int Defence;
        public int Health;
    }
}