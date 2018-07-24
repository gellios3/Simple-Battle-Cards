using Models.Arena;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using View.DeckItems;

namespace View.GameArena
{
    public abstract class DroppableView : EventView, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private BattleSide _battleSide;
        [SerializeField] private Transform _stub;

        public Transform Stub
        {
            get { return _stub; }
            private set { _stub = value; }
        }

        public BattleSide Side => _battleSide;


        public abstract void OnDrop(PointerEventData eventData);

        /// <inheritdoc />
        /// <summary>
        /// On pointer enter
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;
            var draggableCard = eventData.pointerDrag.GetComponent<DraggableView>();

            if (draggableCard == null || draggableCard.Side != Side)
                return;
            var width = draggableCard.GetComponent<LayoutElement>().preferredWidth;
            CreateStub(width);
            draggableCard.PlaceholderParent = transform;
        }

        /// <inheritdoc />
        /// <summary>
        /// On pointer exit
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;

            var draggableCard = eventData.pointerDrag.GetComponent<DraggableView>();

            if (draggableCard == null || draggableCard.PlaceholderParent != transform)
                return;
            DestroyStub();
            draggableCard.PlaceholderParent = draggableCard.ParentToReturnTo;
        }

        /// <summary>
        /// Create Stub
        /// </summary>
        /// <param name="width"></param>
        /// <param name="stubName"></param>
        public void CreateStub(float width, string stubName = "Stub")
        {
            if (Stub != null) return;
            var stub = new GameObject {name = stubName};

            var rectTransform = stub.AddComponent<RectTransform>();
            rectTransform.localScale = Vector3.one;
            rectTransform.sizeDelta = new Vector2(width, 0);

            var le = stub.AddComponent<LayoutElement>();
            le.preferredWidth = width;
            le.preferredHeight = 0;
            le.flexibleWidth = 0;
            le.flexibleHeight = 0;

            Stub = stub.transform;
            Stub.SetParent(transform);
        }

        /// <summary>
        /// Destroy Stub
        /// </summary>
        public void DestroyStub()
        {
            if (Stub != null)
            {
                Destroy(Stub.gameObject);
            }
        }
    }
}