using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDragging : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentToReturn = null;
    public Transform placeHolderParent = null;
    GameObject _placeHolder = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        _placeHolder = new GameObject();
        _placeHolder.transform.SetParent(this.transform.parent);
        LayoutElement _elem = _placeHolder.AddComponent<LayoutElement>();
        _elem.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        _elem.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        _elem.flexibleHeight = 0;
        _elem.flexibleWidth = 0;
        _placeHolder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
        parentToReturn = this.transform.parent;
        placeHolderParent = parentToReturn;
        this.transform.SetParent(this.transform.parent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
        this.transform.position = eventData.position;
        if(_placeHolder.transform.parent!=placeHolderParent)
        {
            _placeHolder.transform.SetParent(placeHolderParent);
        }
        int _newSiblingIndex = placeHolderParent.childCount;
        for (int i = 0; i < placeHolderParent.childCount; i++)
        {
            if (this.transform.position.x < placeHolderParent.GetChild(i).position.x)
            {
                _newSiblingIndex = i;
                if(_placeHolder.transform.GetSiblingIndex()<_newSiblingIndex)
                {
                    _newSiblingIndex--;
                }
                break;
            }
        }
        _placeHolder.transform.SetSiblingIndex(_newSiblingIndex);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        this.transform.SetParent(parentToReturn);
        this.transform.SetSiblingIndex(_placeHolder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Destroy(_placeHolder);
    }

}
