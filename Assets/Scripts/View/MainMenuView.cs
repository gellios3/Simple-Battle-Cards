using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class MainMenuView : EventView
    {
        [SerializeField] private Button _startOnlineGameBtn;
        [SerializeField] private Button _startSingleGameBtn;
        [SerializeField] private InputField _playerName;


        public Button GetOnlineGameBtn()
        {
            return _startOnlineGameBtn;
        }

        public InputField GetPlayerName()
        {
            return _playerName;
        }
    }
}