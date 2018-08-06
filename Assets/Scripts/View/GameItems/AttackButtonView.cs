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

        [SerializeField] private bool _hasActive;

        /// <summary>
        /// On add trate to card
        /// </summary>
        public event Action OnClickBattleItem;

        public bool HasActive
        {
            get { return _hasActive; }
            set { _hasActive = value; }
        }

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
            HasActive = false;
        }

        /// <summary>
        /// Activate attack
        /// </summary>
        public void ActivateAttack()
        {
            HasActive = true;
        }
    }
}