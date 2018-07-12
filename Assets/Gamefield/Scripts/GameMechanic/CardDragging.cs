using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gamefield.Scripts.GameMechanic
{
    public class CardDragging : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        #region Variables
        [SerializeField] private bool _isDraggable = true;
        public Transform ParentToReturn = null;
        public Transform PlaceHolderParent = null;
        private GameObject _startPositionParent = null;
        private GameObject _placeHolder = null;
        private LayoutElement _layoutelem = null;
        private int _playerTurn;
        #endregion

        /// <inheritdoc />
        /// <summary>
        /// Start moving the card
        /// </summary>
        /// <param name="eventData"></param>
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!_isDraggable) return;
            var turn = gameObject.AddComponent<PlayerTurnMode>();
            _playerTurn = turn.PlayerTurn;
            Debug.Log(_playerTurn);
            _placeHolder = new GameObject();
            _startPositionParent = new GameObject();
            _placeHolder.transform.SetParent(this.transform.parent);
            _startPositionParent.transform.SetParent(this.transform.parent);
            var elem = _placeHolder.AddComponent<LayoutElement>();
            elem.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
            elem.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
            elem.flexibleHeight = 0;
            _placeHolder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
            ParentToReturn = this.transform.parent;
            PlaceHolderParent = ParentToReturn;
            this.transform.SetParent(this.transform.parent.parent);
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            _layoutelem = this.GetComponent<LayoutElement>();
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
            this.transform.position = eventData.position;
            if (_placeHolder.transform.parent != PlaceHolderParent)
            {
                _placeHolder.transform.SetParent(PlaceHolderParent);
            }

            var newSiblingIndex = PlaceHolderParent.childCount;
            for (var i = 0; i < PlaceHolderParent.childCount; i++)
            {
                if (!(this.transform.position.x < PlaceHolderParent.GetChild(i).position.x)) continue;
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
                this.transform.SetParent(ParentToReturn);
                this.transform.SetSiblingIndex(_placeHolder.transform.GetSiblingIndex());
                _isDraggable = false;
                Destroy(_placeHolder);
                Destroy(_startPositionParent);
            }
            else if (ParentToReturn.CompareTag("EnemyFinish") && _playerTurn == 2)
            {
                this.transform.SetParent(ParentToReturn);
                this.transform.SetSiblingIndex(_placeHolder.transform.GetSiblingIndex());
                _isDraggable = false;
                Destroy(_placeHolder);
                Destroy(_startPositionParent);
            }
            else
            {
                this.transform.SetParent(_startPositionParent.transform.parent);
                this.transform.SetSiblingIndex(_placeHolder.transform.GetSiblingIndex());
                Destroy(_placeHolder);
                Destroy(_startPositionParent);
            }
        }
    }
}