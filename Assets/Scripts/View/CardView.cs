using Models.Arena;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class CardView : EventView
    {
        [SerializeField] private BattleCard _card;

        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descriptionText;

        [SerializeField] private Image _artworkImage;

        [SerializeField] private TextMeshProUGUI _manaText;
        [SerializeField] private TextMeshProUGUI _attackText;
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private TextMeshProUGUI _defenceText;

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