using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gamefield.Scripts.GameMechanic
{
    public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Text _battleLog;

        /// <inheritdoc />
        /// <summary>
        /// Use to moving card between placeholders
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;

            var drag = eventData.pointerDrag.GetComponent<CardDragging>();
            if (drag != null)
            {
                drag.PlaceHolderParent = transform;
            }

            var drag1 = eventData.pointerDrag.GetComponent<TreitDragging>();
            if (drag1 != null)
            {
                drag1.PlaceHolderParent = transform;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Use to moving card back to start position in one placeholders
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;

            var drag = eventData.pointerDrag.GetComponent<CardDragging>();
            if (drag != null && drag.PlaceHolderParent == transform)
            {
                drag.PlaceHolderParent = drag.ParentToReturn;
            }

            var drag1 = eventData.pointerDrag.GetComponent<TreitDragging>();
            if (drag1 != null && drag1.PlaceHolderParent == transform)
            {
                drag1.PlaceHolderParent = drag1.ParentToReturn;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Fixed moving card on drop zone
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrop(PointerEventData eventData)
        {
            _battleLog.text += eventData.pointerDrag.name + " was dropped on " + gameObject.name;
            var drag = eventData.pointerDrag.GetComponent<CardDragging>();
            if (drag != null)
            {
                drag.ParentToReturn = transform;
            }

            var drag1 = eventData.pointerDrag.GetComponent<TreitDragging>();
            if (drag1 != null)
            {
                drag1.ParentToReturn = transform;
            }
        }
    }
}