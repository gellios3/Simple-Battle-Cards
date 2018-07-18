using UnityEngine;

namespace Models.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Trate", menuName = "Trate")]
    public class Trate : ScriptableObject
    {
        public string Id;

        public Sprite Artwork;

        public int Defence;
        public int Health;

        public int Attack;
        public int Mana;

        public int CriticalChance;
        public float CriticalHit;
    }
}