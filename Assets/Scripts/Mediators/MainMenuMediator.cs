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
        /// On register mediator
        /// </summary>
        public override void OnRegister()
        {
            View.GetOnlineGameBtn().onClick.AddListener(() =>
            {
                var playerName = View.GetPlayerName().text;
                if (playerName.Length <= 0) return;
                // Load lobby scene
                StartOnlineGameSignal.Dispatch(playerName);
                SceneManager.LoadScene("NetworkLobby",LoadSceneMode.Additive);
            });
        }
    }
}