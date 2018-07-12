using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TreitDragging : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private bool _isDraggable = true;
    public Transform parentToReturn = null;
    public Transform placeHolderParent = null;
    GameObject startPositionParent = null;
    GameObject _placeHolder = null;
    LayoutElement _layoutelem = null;
    private int playerTurn;


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_isDraggable)
        {
            PlayerTurnMode _turn = new PlayerTurnMode();
            playerTurn = _turn.PlayerTurn;
            //Debug.Log(playerTurn);
            //Debug.Log("Begin Drag");
            _placeHolder = new GameObject();
            startPositionParent = new GameObject();
            _placeHolder.transform.SetParent(this.transform.parent);
            startPositionParent.transform.SetParent(this.transform.parent);
            LayoutElement _elem = _placeHolder.AddComponent<LayoutElement>();
            _elem.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
            _elem.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
            _elem.flexibleHeight = 0;
//            _elem.flexibleWidth = 0;
            _placeHolder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
            parentToReturn = this.transform.parent;
            placeHolderParent = parentToReturn;
            this.transform.SetParent(this.transform.parent.parent);
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            _layoutelem = this.GetComponent<LayoutElement>();
            _layoutelem.ignoreLayout = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isDraggable)
        {
            //Debug.Log("Drag");
            this.transform.position = eventData.position;
            if (_placeHolder.transform.parent != placeHolderParent)
            {
                _placeHolder.transform.SetParent(placeHolderParent);
            }
            
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_isDraggable)
        {
            //Debug.Log("End Drag");
            // this.transform.SetParent(parentToReturn);
            //this.transform.SetSiblingIndex(_placeHolder.transform.GetSiblingIndex());
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            // Destroy(_placeHolder);
            _layoutelem.ignoreLayout = false;
            if (parentToReturn.tag == "PlayerFinish" && playerTurn == 1)
            {
                this.transform.SetParent(parentToReturn);
                this.transform.SetSiblingIndex(_placeHolder.transform.GetSiblingIndex());
                _isDraggable = false;
                Destroy(_placeHolder);
                Destroy(startPositionParent);
            }
            else if (parentToReturn.tag == "EnemyFinish" && playerTurn == 2)
            {
                this.transform.SetParent(parentToReturn);
                this.transform.SetSiblingIndex(_placeHolder.transform.GetSiblingIndex());
                _isDraggable = false;
                Destroy(_placeHolder);
                Destroy(startPositionParent);
            }
            else
            {
                this.transform.SetParent(startPositionParent.transform.parent);
                this.transform.SetSiblingIndex(_placeHolder.transform.GetSiblingIndex());
                Destroy(_placeHolder);
                Destroy(startPositionParent);
            }
        }
    }
}