using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;
using NetworkPlayer = Models.Miltiplayer.NetworkPlayer;

namespace View.Multiplayer
{
    public class StatusItemView : EventView
    {
        [SerializeField] private Text _title;
        [SerializeField] private NetworkPlayer _networkPlayer;
        [SerializeField] private StatusView _status;

        public void InitPlayer(NetworkPlayer player)
        {
            _networkPlayer = player;
            _title.text = player.Name;
            _status.SetStatusOnline();
        }
    }
}