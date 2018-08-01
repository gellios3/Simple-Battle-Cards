using DG.Tweening;
using strange.extensions.mediation.impl;
using Signals.GameArena;
using UnityEngine;
using UnityEngine.UI;

namespace View.GameArena
{
    public class EndTurnView : EventView
    {
        [SerializeField] private Button _endTurnButton;
        [SerializeField] private Button _expandButton;

        [Inject] public EndTurnSignal EndTurnSignal { get; set; }

        protected override void Start()
        {
            HideEndTurnButton();

            _endTurnButton.onClick.AddListener(() =>
            {
                HideEndTurnButton().onComplete += () => { EndTurnSignal.Dispatch(); };
            });

            _expandButton.onClick.AddListener(ShowEndTurnButton);
        }

        public void ShowEndTurnButton()
        {
            transform.DOLocalMoveX(185, 1, true);
            _expandButton.transform.parent.transform.DOLocalMoveX(-37.5f, 1, true);
        }

        private Tweener HideEndTurnButton()
        {
            _expandButton.transform.parent.transform.DOLocalMoveX(-85, 1, true);
            return transform.DOLocalMoveX(310, 1, true);
        }
    }
}