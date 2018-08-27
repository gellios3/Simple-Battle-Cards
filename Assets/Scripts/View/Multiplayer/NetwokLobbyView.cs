using System.Collections;
using System.Collections.Generic;
using Models.Multiplayer;
using strange.extensions.mediation.impl;
using Signals.multiplayer;
using UnityEngine;
using UnityEngine.UI;

namespace View.Multiplayer
{
    public class NetwokLobbyView : EventView
    {
        /// <summary>
        /// Disconned player from server signal
        /// </summary>
        [Inject]
        public PingPlayerIdToServerSignal PingPlayerIdToServerSignal { get; set; }

        /// <summary>
        ///  Status view
        /// </summary>
        [SerializeField] private StatusView _serverStatus;

        /// <summary>
        /// Start game
        /// </summary>
        [SerializeField] private Button _startGameBtn;

        private readonly List<GameObject> _statusViews = new List<GameObject>();

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
        /// Get start game btn
        /// </summary>
        /// <returns></returns>
        public Button GetStartGameBtn()
        {
            return _startGameBtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="players"></param>
        public void ShowPlayersList(IEnumerable<NetworkLobbyPlayer> players)
        {
            RefreshStatusList();
            foreach (var item in players)
            {
                var statusView = (GameObject) Instantiate(
                    Resources.Load("Prefabs/StatusItem", typeof(GameObject)), new Vector2(), Quaternion.identity,
                    transform
                );
                var statusItemView = statusView.GetComponent<StatusItemView>();
                statusItemView.InitPlayer(item);
                _statusViews.Add(statusView);
            }

            if (_statusViews.Count > 1)
            {
                _startGameBtn.interactable = true;
            }
        }

        /// <summary>
        /// Refresh status list
        /// </summary>
        private void RefreshStatusList()
        {
            foreach (var view in _statusViews)
            {
                Destroy(view);
            }

            _statusViews.Clear();
        }


        private IEnumerator SpawnLoop()
        {
            while (enabled)
            {
                yield return new WaitForSeconds(5);
                PingPlayerIdToServerSignal.Dispatch();
            }
        }
    }
}