using Models.Arena;
using UnityEngine;

namespace View.DeckItems
{
    public class TrateView : DraggableView
    {
        [SerializeField] private BattleTrate _trait;

        public void Init(BattleTrate trate, Transform placeholderParenTransform)
        {
            _trait = trate;
            MainParenTransform = placeholderParenTransform;
            DescriptionText.text = "Card Trate";
            NameText.text = _trait.SourceTrate.name;
            ArtworkImage.sprite = _trait.SourceTrate.Artwork;
            ManaText.text = _trait.Mana.ToString();
            AttackText.text = _trait.Attack.ToString();
            HealthText.text = _trait.Health.ToString();
            DefenceText.text = _trait.Defence.ToString();
        }
       
    }
}