using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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