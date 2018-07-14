using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gamefield.Scripts.GameMechanic
{
    public class CardDragging : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private bool _isDraggable = true;
        public Transform ParentToReturn;
        public Transform PlaceHolderParent;
        private GameObject _startPositionParent;
        private GameObject _placeHolder;
        private LayoutElement _layoutelem;
        private int _playerTurn;


        /// <inheritdoc />
        /// <summary>
        /// Start moving the card
        /// </summary>
        /// <param name="eventData"></param>
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!_isDraggable) return;
//            var turn = gameObject.AddComponent<PlayerTurnMode>();
//            _playerTurn = turn.PlayerTurn;
//            Debug.Log(_playerTurn);
            _placeHolder = new GameObject();
            _startPositionParent = new GameObject();
            _placeHolder.transform.SetParent(transform.parent);
            _startPositionParent.transform.SetParent(transform.parent);
            var elem = _placeHolder.AddComponent<LayoutElement>();
            elem.preferredHeight = GetComponent<LayoutElement>().preferredHeight;
            elem.preferredWidth = GetComponent<LayoutElement>().preferredWidth;
            elem.flexibleHeight = 0;
            _placeHolder.transform.SetSiblingIndex(transform.GetSiblingIndex());
            ParentToReturn = transform.parent;
            PlaceHolderParent = ParentToReturn;
            transform.SetParent(transform.parent.parent);
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            _layoutelem = GetComponent<LayoutElement>();
            _layoutelem.ignoreLayout = true;
        }

        /// <inheritdoc />
        /// <summary>
        /// Dragging the card Logic
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrag(PointerEventData eventData)
        {
            if (!_isDraggable) return;
            transform.position = eventData.position;
            if (_placeHolder.transform.parent != PlaceHolderParent)
            {
                _placeHolder.transform.SetParent(PlaceHolderParent);
            }

            var newSiblingIndex = PlaceHolderParent.childCount;
            for (var i = 0; i < PlaceHolderParent.childCount; i++)
            {
                if (!(transform.position.x < PlaceHolderParent.GetChild(i).position.x)) continue;
                newSiblingIndex = i;
                if (_placeHolder.transform.GetSiblingIndex() < newSiblingIndex)
                {
                    newSiblingIndex--;
                }

                break;
            }

            _placeHolder.transform.SetSiblingIndex(newSiblingIndex);
        }

        /// <inheritdoc />
        /// <summary>
        /// End moving the card
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData eventData)
        {
            if (!_isDraggable) return;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            _layoutelem.ignoreLayout = false;
            if (ParentToReturn.CompareTag("PlayerFinish") && _playerTurn == 1)
            {
                transform.SetParent(ParentToReturn);
                transform.SetSiblingIndex(_placeHolder.transform.GetSiblingIndex());
                _isDraggable = false;
                Destroy(_placeHolder);
                Destroy(_startPositionParent);
            }
            else if (ParentToReturn.CompareTag("EnemyFinish") && _playerTurn == 2)
            {
                transform.SetParent(ParentToReturn);
                transform.SetSiblingIndex(_placeHolder.transform.GetSiblingIndex());
                _isDraggable = false;
                Destroy(_placeHolder);
                Destroy(_startPositionParent);
            }
            else
            {
                transform.SetParent(_startPositionParent.transform.parent);
                transform.SetSiblingIndex(_placeHolder.transform.GetSiblingIndex());
                Destroy(_placeHolder);
                Destroy(_startPositionParent);
            }
        }
    }
}