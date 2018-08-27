using Services.GameArena;
using Signals.MainMenu;
using UnityEngine.SceneManagement;
using View;

namespace Mediators
{
    public class MainMenuMediator : TargetMediator<MainMenuView>
    {
        /// <summary>
        /// Arena initialized signal
        /// </summary>
        [Inject]
        public StartOnlineGameSignal StartOnlineGameSignal { get; set; }

        /// <summary>
        /// Arena initialized signal
        /// </summary>
        [Inject]
        public GameStateService GameStateService { get; set; }

        /// <summary>
        /// On register mediator
        /// </summary>
        public override void OnRegister()
        {
            View.GetOnlineGameBtn().onClick.AddListener(() =>
            {
                var playerName = View.GetPlayerName().text;
                if (playerName.Length <= 0) return;
                GameStateService.InitMultiplayer();
                // Load lobby scene
                StartOnlineGameSignal.Dispatch(playerName);
                SceneManager.LoadScene("NetworkLobby");
            });

            View.GetSingleGameBtn().onClick.AddListener(() =>
            {
//                var playerName = View.GetPlayerName().text;
//                if (playerName.Length <= 0) return;
                GameStateService.InitPlayerName("Player 1");
                GameStateService.InitSingleGame();
                SceneManager.LoadScene("GameArena");
            });
        }
    }
}