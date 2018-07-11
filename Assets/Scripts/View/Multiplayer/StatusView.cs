using UnityEngine;
using UnityEngine.UI;

namespace View.Multiplayer
{
    public class StatusView : MonoBehaviour
    {
        private enum Status
        {
            Online,
            Offline
        }

        [SerializeField] private Text _offlineText;
        [SerializeField] private Text _onlineText;

        [SerializeField] private Status _currentStatus = Status.Offline;

        public void SetStatusOnline()
        {
            _currentStatus = Status.Online;
            _offlineText.gameObject.SetActive(false);
            _onlineText.gameObject.SetActive(true);
        }

        public void SetStatusOffline()
        {
            _currentStatus = Status.Offline;
            _offlineText.gameObject.SetActive(true);
            _onlineText.gameObject.SetActive(false);
        }
    }
}