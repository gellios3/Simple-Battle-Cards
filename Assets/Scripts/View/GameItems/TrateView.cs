using System;
using Models.Arena;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.EventSystems;
using View.AbstractViews;

namespace View.GameItems
{
    public class TrateView : HandItemView
    {
        [SerializeField] private BattleTrate _trate;

        /// <summary>
        /// On add trate to card
        /// </summary>
//        public event Action<TrateView> OnStartDrag;

        public BattleTrate Trate
        {
            get { return _trate; }
            private set { _trate = value; }
        }

        public void Init(BattleTrate trate)
        {
            Trate = trate;
            DescriptionText.text = "Card Trate";
            NameText.text = Trate.SourceTrate.name;
            ArtworkImage.sprite = Trate.SourceTrate.Artwork;
            ManaText.text = Trate.Mana.ToString();
            AttackText.text = Trate.Attack.ToString();
            HealthText.text = Trate.Health.ToString();
            DefenceText.text = Trate.Defence.ToString();
        }

//        /// <inheritdoc />
//        /// <summary>
//        /// On begin drag
//        /// </summary>
//        /// <param name="eventData"></param>
//        public override void OnBeginDrag(PointerEventData eventData)
//        {
//            OnStartDrag?.Invoke(this);
//            base.OnBeginDrag(eventData);
//        }
    }
}