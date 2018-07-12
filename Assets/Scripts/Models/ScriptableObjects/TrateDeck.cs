using System.Collections.Generic;
using UnityEngine;

namespace Models.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New trate deck", menuName = "Trate Deck")]
    public class TrateDeck : ScriptableObject
    {
        public List<Trate> Trates;
    }
}