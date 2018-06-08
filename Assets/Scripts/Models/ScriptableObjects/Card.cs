using System.Collections.Generic;
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

        public int CriticalChance;

        public List<Trate> Trates;
        public CartType Type;
    }

    public enum CartType
    {
        regular,
        epic,
        craft
    }
}