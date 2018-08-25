using System;
using System.Collections.Generic;
using Models;
using Models.Arena;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using View.GameArena;

namespace View.GameItems
{
    public class BattleUnitView : EventView, IPointerDownHandler
    {
        [SerializeField] private BattleCard _card;

        public BattleCard Card
        {
            get { return _card; }
            private set { _card = value; }
        }

        [SerializeField] private BattleSide _battleSide;

        public BattleSide Side
        {
            get { return _battleSide; }
            set { _battleSide = value; }
        }

        [SerializeField] private TextMeshProUGUI _attackText;
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private TextMeshProUGUI _defenceText;
        [SerializeField] private Image _artworkImage;

        [SerializeField] private bool _hasActive;

        public bool HasActive
        {
            get { return _hasActive; }
            set { _hasActive = value; }
        }

        [SerializeField] private bool _hasAttack;

        public bool HasAttack
        {
            get { return _hasAttack; }
            set { _hasAttack = value; }
        }

        /// <summary>
        /// On add trate to card
        /// </summary>
        public event Action<PositionStruct> OnDrawAttackLine;

        /// <summary>
        /// On add trate to card
        /// </summary>
        public event Action OnClickBattleItem;

        /// <summary>
        /// Battle tarates
        /// </summary>
        public List<TrateView> TrateViews { get; } = new List<TrateView>();
        
        /// <inheritdoc />
        /// <summary>
        /// On poiter down
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            OnClickBattleItem?.Invoke();
        }

        /// <summary>
        /// On update
        /// </summary>
        private void Update()
        {
            if (!HasAttack || !HasMouseMoved()) return;
            OnDrawAttackLine?.Invoke(new PositionStruct
            {
                StartPos = transform.position,
                EndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition)
            });
        }

        /// <summary>
        /// Init Card View
        /// </summary>
        /// <param name="card"></param>
        public void Init(BattleCard card)
        {
            Card = card;
            _artworkImage.sprite = Card.SourceCard.Artwork;
            _attackText.text = Card.Attack.ToString();
            _healthText.text = Card.Health.ToString();
            _defenceText.text = Card.Defence.ToString();
        }

        /// <summary>
        /// Add trate to battle card
        /// </summary>
        /// <param name="trateView"></param>
        public void AddTrate(TrateView trateView)
        {
            Card.Defence += trateView.Trate.Defence;
            Card.Health += trateView.Trate.Health;
            Card.Attack += trateView.Trate.Attack;
            Card.CriticalChance += trateView.Trate.CriticalChance;
            Card.CriticalHit += trateView.Trate.CriticalHit;
            TrateViews.Add(trateView);
            // Show card on battle arena
            Init(Card);
            trateView.DestroyView();
        }

        /// <summary>
        /// Destroy мiew
        /// </summary>
        public void DestroyView()
        {
            Destroy(gameObject);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool HasMouseMoved()
        {
            //I feel dirty even doing this 
            return Math.Abs(Input.GetAxis("Mouse X")) > 0 || Math.Abs(Input.GetAxis("Mouse Y")) > 0;
        }
    }
}