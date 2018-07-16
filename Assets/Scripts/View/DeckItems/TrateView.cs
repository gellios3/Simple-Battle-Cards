using Models.Arena;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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