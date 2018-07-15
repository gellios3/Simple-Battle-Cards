using Models.Arena;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace View.GameArena
{
    public class SideDecksView : EventView
    {
        [SerializeField] private BattleSide _battleSide;
        [SerializeField] private Text _cardDeckCountText;
        [SerializeField] private Text _trateDeckCountText;

        public BattleSide GetCurrentSide()
        {
            return _battleSide;
        }

        public void SetCardDeckCount(int count)
        {
            _cardDeckCountText.text = count.ToString();
        }
        
        public void SetTrateDeckCount(int count)
        {
            _trateDeckCountText.text = count.ToString();
        }
    }
}