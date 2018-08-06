using System;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View.GameItems
{
    public class AttackButtonView : EventView
    {
        [SerializeField] private Button _attackBtn;

        /// <summary>
        /// On add trate to card
        /// </summary>
        public event Action OnClickBattleItem;

        /// <inheritdoc />
        /// <summary>
        /// On Start
        /// </summary>
        protected override void Start()
        {
            _attackBtn.onClick.AddListener(() => { OnClickBattleItem?.Invoke(); });
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