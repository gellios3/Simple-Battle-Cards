using System.Collections;
using DG.Tweening;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;

namespace View.GameArena
{
    public class TurnPopupView : EventView
    {
        [SerializeField] private TextMeshProUGUI _textMeshProUGui;

        // Use this for initialization
        public void ShowPopup(string text)
        {
            StartCoroutine(StartShowPopup(text));
        }


        private IEnumerator StartShowPopup(string text)
        {
            yield return new WaitForSeconds(1);
            _textMeshProUGui.gameObject.SetActive(true);
            _textMeshProUGui.text = text;
            _textMeshProUGui.transform.DOScale(Vector3.one, 1);
            _textMeshProUGui.DOFade(1, 1).onComplete += () =>
            {
                _textMeshProUGui.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1).onComplete += () =>
                {
                    _textMeshProUGui.transform.DOScale(new Vector3(2, 2, 2), 1);
                    _textMeshProUGui.DOFade(0, 1).onComplete += () => { _textMeshProUGui.gameObject.SetActive(false); };
                };
            };
        }
    }
}