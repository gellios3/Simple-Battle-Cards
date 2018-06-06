using System.Collections.Generic;
using UnityEngine;

namespace Models.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New deck", menuName = "Deck")]
    public class Deck : ScriptableObject
    {
        public List<Card> Cards;
        public List<Trate> Trates;
    }
}