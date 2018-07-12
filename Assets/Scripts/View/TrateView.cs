using Models.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class TrateView : MonoBehaviour
    {
        [SerializeField] private Trate _trait;

        [SerializeField] private Text _nameText;
        [SerializeField] private Text _descriptionText;

        [SerializeField] private Image _artworkImage;

        [SerializeField] private Text _manaText;
        [SerializeField] private Text _attackText;
        [SerializeField] private Text _healthText;
        [SerializeField] private Text _defenceText;

        private void Start()
        {
            _nameText.text = _trait.name;
           
            _artworkImage.sprite = _trait.Artwork;

            _manaText.text = _trait.Defence.ToString();
            _attackText.text = _trait.Attack.ToString();
            _healthText.text = _trait.Health.ToString();
            _defenceText.text = _trait.Defence.ToString();
        }
    }
}