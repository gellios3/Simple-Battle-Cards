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
            set { _card = value; }
        }

        /// <summary>
        /// Init Card View
        /// </summary>
        /// <param name="card"></param>
        /// <param name="placeholderParenTransform"></param>
        public void Init(BattleCard card, Transform placeholderParenTransform)
        {
            Card = card;
            MainParenTransform = placeholderParenTransform;
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
        /// On drop on card trate or Enemy Card
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrop(PointerEventData eventData)
        {
            var draggableTrateView = eventData.pointerDrag.GetComponent<TrateView>();
            if (draggableTrateView != null)
            {
                // @todo Call add trate to card
                Debug.Log("On trate drop");
                Destroy(draggableTrateView.Placeholder);
                Destroy(eventData.pointerDrag);
            }

            var draggableCard = eventData.pointerDrag.GetComponent<CardView>();
            if (draggableCard != null)
            {
                // @todo Call hit card
                Debug.Log("On card drop");
            }
        }
    }
}