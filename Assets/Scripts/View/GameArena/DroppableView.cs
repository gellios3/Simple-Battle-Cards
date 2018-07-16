using Models.Arena;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.EventSystems;

namespace View.GameArena
{
    public abstract class DroppableView : EventView, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private BattleSide _battleSide;

        public BattleSide Side => _battleSide;

        public abstract void OnDrop(PointerEventData eventData);
        public abstract void OnPointerEnter(PointerEventData eventData);
        public abstract void OnPointerExit(PointerEventData eventData);
    }
}