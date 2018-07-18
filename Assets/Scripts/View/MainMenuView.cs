using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class MainMenuView : EventView
    {
        [SerializeField] private Button _startOnlineGameBtn;
        [SerializeField] private Button _startSingleGameBtn;
        [SerializeField] private TMP_InputField _playerName;

        public Button GetOnlineGameBtn()
        {
            return _startOnlineGameBtn;
        }

        public Button GetSingleGameBtn()
        {
            return _startSingleGameBtn;
        }

        public TMP_InputField GetPlayerName()
        {
            return _playerName;
        }
    }
}