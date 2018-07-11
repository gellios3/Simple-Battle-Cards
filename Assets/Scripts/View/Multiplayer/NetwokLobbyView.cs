using strange.extensions.mediation.impl;
using UnityEngine;

namespace View.Multiplayer
{
    public class NetwokLobbyView : EventView
    {
        [SerializeField] private StatusView _serverStatus;
        [SerializeField] private StatusView _payerOneStatus;
        [SerializeField] private StatusView _payerTwoStatus;

        /// <summary>
        /// On server connected
        /// </summary>
        public void OnServerConnected()
        {
            _serverStatus.SetStatusOnline();
        }

        /// <summary>
        /// On disconect from server
        /// </summary>
        public void OnServerDisconnected()
        {
            _serverStatus.SetStatusOffline();
        }

        /// <summary>
        /// On current player connected
        /// </summary>
        public void OnCurrentPlayerConnected()
        {
            _payerOneStatus.SetStatusOnline();
        }

        /// <summary>
        /// On opponent connected
        /// </summary>
        public void OnOpponentConnected()
        {
            _payerTwoStatus.SetStatusOnline();
        }
    }
}