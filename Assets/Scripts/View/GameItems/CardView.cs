using System;
using Models.Arena;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using View.AbstractViews;

namespace View.GameItems
{
    public class CardView : DraggableView, IPointerClickHandler
    {
        [SerializeField] private BattleCard _card;
 

        public BattleCard Card
        {
            get { return _card ?? (_card = new BattleCard()); }
            private set { _card = value; }
        }

        /// <summary>
        /// On add trate to card
        /// </summary>
        public event Action<CardView> OnStartDrag;

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

        public void OnPointerClick(PointerEventData eventData)
        {
            ZoomOutAnimation();
        }
    }
}