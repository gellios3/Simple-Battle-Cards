using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;

namespace View.GameArena
{
    public class HistoryLogView : EventView
    {
        [SerializeField] private TextMeshProUGUI _historyLog;

        public void AddHistoryLog(string str)
        {
            _historyLog.text += str;
        }
    }
}