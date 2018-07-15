using Models.Arena;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace View.GameArena
{
    public class TrateDeskView : EventView
    {
        [SerializeField] private BattleSide _battleSide;
        [SerializeField] private Text _trateDeckCountText;

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