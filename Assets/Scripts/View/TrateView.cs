using Models.Arena;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class TrateView : EventView
    {
        [SerializeField] private BattleTrate _trait;
        [SerializeField] private Text _nameText;
        [SerializeField] private Text _descriptionText;
        [SerializeField] private Image _artworkImage;
        [SerializeField] private Text _manaText;
        [SerializeField] private Text _attackText;
        [SerializeField] private Text _healthText;
        [SerializeField] private Text _defenceText;

        public void Init(BattleTrate trate)
        {
            _trait = trate;
            _nameText.text = _trait.SourceTrate.name;
            _artworkImage.sprite = _trait.SourceTrate.Artwork;
            _manaText.text = _trait.Defence.ToString();
            _attackText.text = _trait.Attack.ToString();
            _healthText.text = _trait.Health.ToString();
            _defenceText.text = _trait.Defence.ToString();
        }
    }
}