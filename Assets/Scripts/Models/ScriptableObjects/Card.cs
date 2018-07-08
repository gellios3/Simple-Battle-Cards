using UnityEngine;

namespace Models.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        public new string name;

        public string Id;
        public string Description;

        public Sprite Artwork;

        public int Defence;
        public int Attack;
        public int Health;

        public int Mana;

        /// <summary>
        /// Critical chance
        /// </summary>
        public int CriticalChance;

        /// <summary>
        /// Critical hit
        /// </summary>
        public float CriticalHit;
    }
}