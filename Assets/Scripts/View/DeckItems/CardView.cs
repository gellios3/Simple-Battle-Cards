﻿﻿using System;
using System.Collections.Generic;
using DG.Tweening;
using Models.Arena;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View.DeckItems
{
    public class CardView : DraggableView, IDropHandler
    {
        [SerializeField] private BattleCard _card;

        [SerializeField] protected Image StubImage;

        public BattleCard Card
        {
            get { return _card; }
            private set { _card = value; }
        }

        /// <summary>
        /// On add trate to card
        /// </summary>
        public event Action<TrateView> OnAddTrateToCard;

        /// <summary>
        /// On add trate to card
        /// </summary>
        public event Action<CardView> OnStartDrag;

        /// <summary>
        /// On add trate to card
        /// </summary>
        public event Action<CardView> OnTakeDamage;
        
        /// <summary>
        /// On add trate to card
        /// </summary>
        public event Action<CardView> OnAddToHand;

        /// <summary>
        /// Battle tarates
        /// </summary>
        public List<TrateView> TrateViews { get; } = new List<TrateView>();

        private readonly Vector3[] _waypoints = new Vector3[2];

        /// <summary>
        /// Init Card View
        /// </summary>
        /// <param name="card"></param>
        public void Init(BattleCard card)
        {
            Card = card;
            NameText.text = Card.SourceCard.name;
            DescriptionText.text = Card.SourceCard.Description;
            ArtworkImage.sprite = Card.SourceCard.Artwork;
            ManaText.text = Card.Mana.ToString();
            AttackText.text = Card.Attack.ToString();
            HealthText.text = Card.Health.ToString();
            DefenceText.text = Card.Defence.ToString();
        }

        /// <summary>
        /// Start path animation
        /// </summary>
        public void StartPathAnimation()
        {
            _waypoints[0] = ParentToReturnTo.position;
            _waypoints[1] = ParentToReturnTo.Find("Stub").transform.position;
            var path = transform.DOPath(_waypoints, 2, PathType.CatmullRom);
            path.onPlay += OnStartAnimation;
            path.onComplete += OnCompleteAnimation;
        }

        /// <summary>
        /// Toogle stub image
        /// </summary>
        /// <param name="status"></param>
        public void ToogleStubImage(bool status)
        {
            StubImage.gameObject.SetActive(status);
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
            Destroy(Placeholder);
            Destroy(gameObject);
        }

        /// <inheritdoc />
        /// <summary>
        /// On begin drag
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnBeginDrag(PointerEventData eventData)
        {
            OnStartDrag?.Invoke(this);
            base.OnBeginDrag(eventData);
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

            var draggableCard = eventData.pointerDrag.GetComponent<CardView>();
            if (draggableCard != null)
            {
                OnTakeDamage?.Invoke(draggableCard);
            }
        }

        /// <summary>
        /// On complete animation
        /// </summary>
        private void OnCompleteAnimation()
        {
            transform.SetParent(ParentToReturnTo);
            transform.SetSiblingIndex(transform.parent.childCount - 1);
            OnAddToHand?.Invoke(this);
        }

        /// <summary>
        /// On start animation
        /// </summary>
        private void OnStartAnimation()
        {
            transform.SetParent(MainParenTransform);
        }
    }
}