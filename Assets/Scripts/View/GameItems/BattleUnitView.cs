using System;
using System.Collections.Generic;
using Models.Arena;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View.GameItems
{
    public class BattleUnitView : EventView, IDropHandler
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

        [SerializeField] protected TextMeshProUGUI AttackText;
        [SerializeField] protected TextMeshProUGUI HealthText;
        [SerializeField] protected TextMeshProUGUI DefenceText;
        [SerializeField] protected Image ArtworkImage;

        /// <summary>
        /// On add trate to card
        /// </summary>
        public event Action<TrateView> OnAddTrateToCard;

        /// <summary>
        /// On add trate to card
        /// </summary>
        public event Action<BattleUnitView> OnTakeDamage;

        /// <summary>
        /// Battle tarates
        /// </summary>
        public List<TrateView> TrateViews { get; } = new List<TrateView>();
        
//        private void Update()
//        {
//            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            Debug.DrawLine(transform.position, mouseWorldPos, Color.red);
//        }

        /// <summary>
        /// Init Card View
        /// </summary>
        /// <param name="card"></param>
        public void Init(BattleCard card)
        {
            Card = card;
            ArtworkImage.sprite = Card.SourceCard.Artwork;
            AttackText.text = Card.Attack.ToString();
            HealthText.text = Card.Health.ToString();
            DefenceText.text = Card.Defence.ToString();
        }

        /// <inheritdoc />
        /// <summary>
        /// On drop on card trate or Enemy Card
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrop(PointerEventData eventData)
        {
            if (Card.Status == BattleStatus.Sleep || Card.Status == BattleStatus.Active)
            {
                var trateView = eventData.pointerDrag.GetComponent<TrateView>();
                if (trateView != null)
                {
                    if (trateView.Side == Side)
                    {
                        OnAddTrateToCard?.Invoke(trateView);
                    }
                }
            }

            var draggableCard = eventData.pointerDrag.GetComponent<BattleUnitView>();
            if (draggableCard != null)
            {
                OnTakeDamage?.Invoke(draggableCard);
            }
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
    }
}