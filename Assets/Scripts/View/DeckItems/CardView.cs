using Models.Arena;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View.DeckItems
{
    public class CardView : DraggableView
    {
        [SerializeField] private BattleCard _card;

        public void Init(BattleCard card, Transform placeholderParenTransform)
        {
            _card = card;
            MainParenTransform = placeholderParenTransform;
            NameText.text = _card.SourceCard.name;
            DescriptionText.text = _card.SourceCard.Description;
            ArtworkImage.sprite = _card.SourceCard.Artwork;
            ManaText.text = _card.Mana.ToString();
            AttackText.text = _card.Attack.ToString();
            HealthText.text = _card.Health.ToString();
            DefenceText.text = _card.Defence.ToString();
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            Placeholder = new GameObject();
            Placeholder.transform.SetParent(transform.parent);

            var le = Placeholder.AddComponent<LayoutElement>();
            le.preferredWidth = GetComponent<LayoutElement>().preferredWidth;
            le.preferredHeight = 0;
            le.flexibleWidth = 0;
            le.flexibleHeight = 0;

            Placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());

            // Set plasholder zero height 
            Placeholder.GetComponent<RectTransform>().sizeDelta =
                new Vector2(GetComponent<LayoutElement>().preferredWidth, 0);
            Placeholder.transform.localScale = new Vector3(1, 1, 1);

            ParentToReturnTo = transform.parent;
            PlaceholderParent = ParentToReturnTo;
            transform.SetParent(MainParenTransform);
            GetComponent<CanvasGroup>().blocksRaycasts = false;

            transform.position = new Vector3(eventData.position.x, eventData.position.y, 1);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            var pos = Camera.main.ScreenToWorldPoint(eventData.position);
            transform.position = new Vector3(pos.x, pos.y, 1);
            if (Placeholder.transform.parent != Placeholder)
            {
                Placeholder.transform.SetParent(PlaceholderParent);
            }

            var newSiblingIndex = PlaceholderParent.childCount;
            for (var i = 0; i < PlaceholderParent.childCount; i++)
            {
                if (!(transform.position.x < PlaceholderParent.GetChild(i).position.x)) continue;
                newSiblingIndex = i;
                if (Placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                {
                    newSiblingIndex--;
                }

                break;
            }

            Placeholder.transform.SetSiblingIndex(newSiblingIndex);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            transform.SetParent(ParentToReturnTo);
            transform.SetSiblingIndex(Placeholder.transform.GetSiblingIndex());
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            Destroy(Placeholder);
        }
    }
}