using System.Collections.Generic;
using UnityEngine;

namespace Models.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New cart deck", menuName = "Cart Deck")]
    public class CartDeck : ScriptableObject
    {
        public List<Card> Cards;
    }
}