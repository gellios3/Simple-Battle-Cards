using Models.Arena;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class CardView : EventView
    {
        [SerializeField] private BattleCard _card;

        [SerializeField] private Text _nameText;
        [SerializeField] private Text _descriptionText;

        [SerializeField] private Image _artworkImage;

        [SerializeField] private Text _manaText;
        [SerializeField] private Text _attackText;
        [SerializeField] private Text _healthText;
        [SerializeField] private Text _defenceText;

        public void Init(BattleCard card)
        {
            _card = card;
            _nameText.text = _card.SourceCard.name;
            _descriptionText.text = _card.SourceCard.Description;
            _artworkImage.sprite = _card.SourceCard.Artwork;
            _manaText.text = _card.Defence.ToString();
            _attackText.text = _card.Attack.ToString();
            _healthText.text = _card.Health.ToString();
            _defenceText.text = _card.Defence.ToString();
        }
    }
}