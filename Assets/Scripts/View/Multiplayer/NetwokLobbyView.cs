using System.Collections;
using strange.extensions.mediation.impl;
using Signals.multiplayer;
using UnityEngine;

namespace View.Multiplayer
{
    public class NetwokLobbyView : EventView
    {
        /// <summary>
        /// Disconned player from server signal
        /// </summary>
        [Inject] public PingPlayerIdToServerSignal PingPlayerIdToServerSignal { get; set; }
        
        [SerializeField] private StatusView _serverStatus;
        [SerializeField] private StatusView _payerOneStatus;
        [SerializeField] private StatusView _payerTwoStatus;

        private void Awake()
        {
            StartCoroutine(SpawnLoop());
        }

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

        private IEnumerator SpawnLoop()
        {
            while (enabled)
            {
                yield return new WaitForSeconds (5);
                PingPlayerIdToServerSignal.Dispatch();
            }
        }
    }
}