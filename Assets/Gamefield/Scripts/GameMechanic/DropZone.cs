using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Gamefield.Scripts.GameMechanic;

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
            //Debug.Log("OnPointerEnter");
            if (eventData.pointerDrag == null)
                return;

            CardDragging drag = eventData.pointerDrag.GetComponent<CardDragging>();
            if (drag != null)
            {
                drag.PlaceHolderParent = this.transform;
            }
            TreitDragging drag1 = eventData.pointerDrag.GetComponent<TreitDragging>();
            if (drag1 != null)
            {
                drag1.placeHolderParent = this.transform;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Use to moving card back to start position in one placeholders
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            //Debug.Log("OnPointerExit");
            if (eventData.pointerDrag == null)
                return;

            CardDragging drag = eventData.pointerDrag.GetComponent<CardDragging>();
            if (drag != null && drag.PlaceHolderParent == this.transform)
            {
                drag.PlaceHolderParent = drag.ParentToReturn;
            }
            TreitDragging drag1 = eventData.pointerDrag.GetComponent<TreitDragging>();
            if (drag1 != null && drag1.placeHolderParent == this.transform)
            {
                drag1.placeHolderParent = drag1.parentToReturn;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Fixed moving card on drop zone
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);
            _battleLog.text += eventData.pointerDrag.name + " was dropped on " + gameObject.name;
            var drag = eventData.pointerDrag.GetComponent<CardDragging>();
            if (drag != null)
            {
                drag.ParentToReturn = this.transform;
            }
            var drag1 = eventData.pointerDrag.GetComponent<TreitDragging>();
            if (drag1 != null)
            {
                drag1.parentToReturn = this.transform;
            }
        }
    }
}
