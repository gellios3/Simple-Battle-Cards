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

        /// <summary>
        /// Defence
        /// </summary>
        public int Defence;

        /// <summary>
        /// Attack
        /// </summary>
        public int Attack;

        /// <summary>
        /// Health
        /// </summary>
        public int Health;

        /// <summary>
        /// Mana
        /// </summary>
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