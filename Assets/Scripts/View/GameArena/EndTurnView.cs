using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace View.GameArena
{
    public class EndTurnView : EventView
    {
        [SerializeField] private Button _endTurnButton;

        public Button EndTurnButton => _endTurnButton;
    }
}