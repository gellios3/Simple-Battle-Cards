using System;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View.GameItems
{
    public class AttackButtonView : EventView, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Button _attackBtn;
        [SerializeField] private bool _hasEnterOponentUnit;

        public bool HasEnterOponentUnit
        {
            private get { return _hasEnterOponentUnit; }
            set { _hasEnterOponentUnit = value; }
        }

        /// <summary>
        /// On add trate to card
        /// </summary>
        public event Action<bool> OnInitTakeDamage;

        /// <summary>
        /// On add trate to card
        /// </summary>
        public event Action OnInitAttack;

        /// <summary>
        /// On add trate to card
        /// </summary>
        public event Action OnTakeDamage;

        /// <inheritdoc />
        /// <summary>
        /// On Start
        /// </summary>
        protected override void Start()
        {
            _attackBtn.onClick.AddListener(() =>
            {
                if (HasEnterOponentUnit)
                {
                    OnTakeDamage?.Invoke();
                }
                else
                {
                    OnInitAttack?.Invoke();
                }
            });
        }

        /// <inheritdoc />
        /// <summary>
        /// On pointer enter
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            OnInitTakeDamage?.Invoke(true);
        }

        /// <inheritdoc />
        /// <summary>
        /// On poiter enter
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            OnInitTakeDamage?.Invoke(false);
        }

        /// <summary>
        /// Deactivate attack
        /// </summary>
        public void DeactivateAttack()
        {
            _attackBtn.interactable = false;
        }

        /// <summary>
        /// Activate attack
        /// </summary>
        public void ActivateAttack()
        {
            _attackBtn.interactable = true;
        }
    }
}