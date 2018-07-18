using Models.Arena;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;

namespace View.GameArena
{
    public class TrateDeskView : EventView
    {
        [SerializeField] private BattleSide _battleSide;
        [SerializeField] private TextMeshProUGUI _trateDeckCountText;

        public BattleSide GetCurrentSide()
        {
            return _battleSide;
        }

        public void SetTrateDeckCount(int count)
        {
            _trateDeckCountText.text = count.ToString();
        }
    }
}