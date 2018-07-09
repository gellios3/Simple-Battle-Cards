using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointerEnter");
        if (eventData.pointerDrag == null)
            return;

        CardDragging drag = eventData.pointerDrag.GetComponent<CardDragging>();
        if (drag != null)
        {
            drag.placeHolderParent = this.transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("OnPointerExit");
        if (eventData.pointerDrag == null)
            return;

        CardDragging drag = eventData.pointerDrag.GetComponent<CardDragging>();
        if (drag != null && drag.placeHolderParent == this.transform)
        {
            drag.placeHolderParent = drag.parentToReturn;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);

        CardDragging drag = eventData.pointerDrag.GetComponent<CardDragging>();
        if (drag != null)
        {
            drag.parentToReturn = this.transform;
        }

    }
}
