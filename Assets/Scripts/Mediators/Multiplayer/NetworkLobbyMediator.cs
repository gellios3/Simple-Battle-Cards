using Services.Multiplayer;
using Signals.multiplayer;
using UnityEngine.SceneManagement;
using View.Multiplayer;

namespace Mediators.Multiplayer
{
    public class NetworkLobbyMediator : TargetMediator<NetwokLobbyView>
    {
        /// <summary>
        /// Arena initialized signal
        /// </summary>
        [Inject]
        public ServerConnectedSignal ServerConnectedSignal { get; set; }

        /// <summary>
        /// Disconnected from server signal
        /// </summary>
        [Inject]
        public DisconnectedFromServerSignal DisconnectedFromServerSignal { get; set; }

        [Inject] public ShowLobbyPlayersSignal ShowLobbyPlayersSignal { get; set; }

        [Inject] public NetworkPlayerService NetworkPlayerService { get; set; }

        /// <summary>
        /// On register mediator
        /// </summary>
        public override void OnRegister()
        {
            View.GetStartGameBtn().onClick.AddListener(() => { SceneManager.LoadScene("GameArena"); });
            ServerConnectedSignal.AddListener(() => { View.OnServerConnected(); });
            DisconnectedFromServerSignal.AddListener(() => { View.OnServerDisconnected(); });
            ShowLobbyPlayersSignal.AddListener(() => { View.ShowPlayersList(NetworkPlayerService.OnlinePlayers); });
        }
    }
}