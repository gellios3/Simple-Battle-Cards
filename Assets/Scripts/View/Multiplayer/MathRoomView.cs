using strange.extensions.mediation.impl;
using UnityEngine;

namespace View.Multiplayer
{
    public class MathRoomView : EventView
    {
        [SerializeField] private StatusView _serverStatus;
        [SerializeField] private StatusView _payerOneStatus;
        [SerializeField] private StatusView _payerTwoStatus;

        /// <summary>
        /// On server connected
        /// </summary>
        public void OnServerConnected()
        {
            _serverStatus.ToggleStatus();
            _payerOneStatus.ToggleStatus();
        }

        /// <summary>
        /// On opponent connected
        /// </summary>
        public void OnOpponentConnected()
        {
            _payerTwoStatus.ToggleStatus();
        }
    }
}