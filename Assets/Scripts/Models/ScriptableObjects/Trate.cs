using UnityEngine;

namespace Models.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Trate", menuName = "Trate")]
    public class Trate : ScriptableObject
    {
        public int Defence;
        public int Health;
    }
}