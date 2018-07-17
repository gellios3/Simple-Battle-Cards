using System;
using Models.Arena;
using UnityEngine;
using UnityEngine.EventSystems;

namespace View.DeckItems
{
    public class CardView : DraggableView, IDropHandler
    {
        [SerializeField] private BattleCard _card;

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
        /// Init Card View
        /// </summary>
        /// <param name="card"></param>
        public void Init(BattleCard card)
        {
            Card = card;
            Item = card;
            NameText.text = Card.SourceCard.name;
            DescriptionText.text = Card.SourceCard.Description;
            ArtworkImage.sprite = Card.SourceCard.Artwork;
            ManaText.text = Card.Mana.ToString();
            AttackText.text = Card.Attack.ToString();
            HealthText.text = Card.Health.ToString();
            DefenceText.text = Card.Defence.ToString();
        }

        
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
    }
}