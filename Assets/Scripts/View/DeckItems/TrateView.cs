using Models.Arena;
using UnityEngine;

namespace View.DeckItems
{
    public class TrateView : DraggableView
    {
        [SerializeField] private BattleTrate _trate;

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

        public void DestroyView()
        {
            Destroy(Placeholder);
            Destroy(gameObject);
        }
       
    }
}