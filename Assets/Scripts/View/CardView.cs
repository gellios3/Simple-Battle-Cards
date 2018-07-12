using Models.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private Card _card;

        [SerializeField] private Text _nameText;
        [SerializeField] private Text _descriptionText;

        [SerializeField] private Image _artworkImage;

        [SerializeField] private Text _manaText;
        [SerializeField] private Text _attackText;
        [SerializeField] private Text _healthText;
        [SerializeField] private Text _defenceText;

        // Use this for initialization
        private void Start()
        {
            _nameText.text = _card.name;
            _descriptionText.text = _card.Description;

            _artworkImage.sprite = _card.Artwork;

            _manaText.text = _card.Defence.ToString();
            _attackText.text = _card.Attack.ToString();
            _healthText.text = _card.Health.ToString();
            _defenceText.text = _card.Defence.ToString();
        }
    }
}