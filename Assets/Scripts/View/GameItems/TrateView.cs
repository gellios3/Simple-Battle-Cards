using System;
using Models;
using Models.Arena;
using UnityEngine;
using UnityEngine.EventSystems;
using View.AbstractViews;
using Debug = System.Diagnostics.Debug;

namespace View.GameItems
{
    public class TrateView : HandItemView, IPointerDownHandler
    {
        [SerializeField] private BattleTrate _trate;

        public bool HasApplied;

        /// <summary>
        /// On add trate to card
        /// </summary>
        public event Action OnInitApply;

        /// <summary>
        /// On add trate to card
        /// </summary>
        public event Action<PositionStruct> OnDrawAttackLine;

        public BattleTrate Trate
        {
            get { return _trate; }
            private set { _trate = value; }
        }

        /// <summary>
        /// On update
        /// </summary>
        private void Update()
        {
            if (!HasApplied || !HasMouseMoved())
                return;

            if (Camera.main != null)
            {
                OnDrawAttackLine?.Invoke(new PositionStruct
                {
                    StartPos = transform.position,
                    EndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition)
                });
            }
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="trate"></param>
        public void Init(BattleTrate trate)
        {
            Trate = trate;
            DescriptionText.text = "Card Trate";
            NameText.text = Trate.SourceTrate.name;
            ArtworkImage.sprite = Trate.SourceTrate.Artwork;
            ManaText.text = Trate.Mana.ToString();
            AttackText.text = Trate.Attack.ToString();
            HealthText.text = Trate.Health.ToString();
            DefenceText.text = Trate.Defence.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool HasMouseMoved()
        {
            return Math.Abs(Input.GetAxis("Mouse X")) > 0 || Math.Abs(Input.GetAxis("Mouse Y")) > 0;
        }

        /// <inheritdoc />
        /// <summary>
        /// On poiter down
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            OnInitApply?.Invoke();
        }
    }
}