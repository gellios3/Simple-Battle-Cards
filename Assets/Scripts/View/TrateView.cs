using Models.Arena;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class TrateView : EventView
    {
        [SerializeField] private BattleTrate _trait;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private Image _artworkImage;
        [SerializeField] private TextMeshProUGUI _manaText;
        [SerializeField] private TextMeshProUGUI _attackText;
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private TextMeshProUGUI _defenceText;

        public void Init(BattleTrate trate)
        {
            _trait = trate;
            _descriptionText.text = "Card Trate";
            _nameText.text = _trait.SourceTrate.name;
            _artworkImage.sprite = _trait.SourceTrate.Artwork;
            _manaText.text = _trait.Defence.ToString();
            _attackText.text = _trait.Attack.ToString();
            _healthText.text = _trait.Health.ToString();
            _defenceText.text = _trait.Defence.ToString();
        }
    }
}