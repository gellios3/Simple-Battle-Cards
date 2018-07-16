using Models.Arena;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.GameArena
{
    public class CardDeckView : EventView
    {
        [SerializeField] private BattleSide _battleSide;
        [SerializeField] private TextMeshProUGUI _cardDeckCountText;

        public BattleSide GetCurrentSide()
        {
            return _battleSide;
        }

        public void SetCardDeckCount(int count)
        {
            _cardDeckCountText.text = count.ToString();
        }
    }
}