using Models.Arena;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace View.GameArena
{
    public class CardDeckView : EventView
    {
        [SerializeField] private BattleSide _battleSide;
        [SerializeField] private Text _deckCounText;

        public BattleSide GetCurrentSide()
        {
            return _battleSide;
        }

        public void SetDeckCount(int count)
        {
            _deckCounText.text = count.ToString();
        }
    }
}