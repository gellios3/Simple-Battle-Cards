using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace View.GameArena
{
    public class HistoryLogView : EventView
    {
        [SerializeField] private Text _historyLog;

        public void AddHistoryLog(string str)
        {
            _historyLog.text += str;
        }
    }
}