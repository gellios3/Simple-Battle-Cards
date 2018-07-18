using System;
using Models.Arena;
using UnityEngine;
using UnityEngine.EventSystems;

namespace View.DeckItems
{
    public class TrateView : DraggableView
    {
        [SerializeField] private BattleTrate _trate;
        
        /// <summary>
        /// On add trate to card
        /// </summary>
        public event Action<TrateView> OnStartDrag;

        public BattleTrate Trate
        {
            get { return _trate; }
            private set { _trate = value; }
        }

        public void Init(BattleTrate trate)
        {
            Trate = trate;
            Item = trate;
            DescriptionText.text = "Card Trate";
            NameText.text = Trate.SourceTrate.name;
            ArtworkImage.sprite = Trate.SourceTrate.Artwork;
            ManaText.text = Trate.Mana.ToString();
            AttackText.text = Trate.Attack.ToString();
            HealthText.text = Trate.Health.ToString();
            DefenceText.text = Trate.Defence.ToString();
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
       
    }
}