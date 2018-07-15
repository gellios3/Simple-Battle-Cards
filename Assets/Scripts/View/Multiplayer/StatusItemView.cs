using Models.Miltiplayer;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace View.Multiplayer
{
    public class StatusItemView : EventView
    {
        [SerializeField] private Text _title;
        [SerializeField] private NetworkLobbyPlayer _networkLobbyPlayer;
        [SerializeField] private StatusView _status;

        public void InitPlayer(NetworkLobbyPlayer lobbyPlayer)
        {
            _networkLobbyPlayer = lobbyPlayer;
            _title.text = lobbyPlayer.Name;
            _status.SetStatusOnline();
        }
    }
}