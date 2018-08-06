using System;
using Models;
using Models.Arena;
using UnityEngine;
using UnityEngine.UI;
using View.AbstractViews;

namespace View.GameItems
{
    public class TrateView : HandItemView
    {
        [SerializeField] private BattleTrate _trate;
        [SerializeField] private Button _button;

        public bool HasApplyed;

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

        /// <inheritdoc />
        /// <summary>
        /// On Start view
        /// </summary>
        protected override void Start()
        {
            _button.onClick.AddListener(() =>
            {
                OnInitApply?.Invoke();
                ZoomOutAnimation();
            });
        }
        
        /// <summary>
        /// On update
        /// </summary>
        private void Update()
        {
            if (!HasApplyed || !HasMouseMoved()) return;
            OnDrawAttackLine?.Invoke(new PositionStruct
            {
                StartPos = transform.position,
                EndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition)
            });
        }

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
            //I feel dirty even doing this 
            return Math.Abs(Input.GetAxis("Mouse X")) > 0 || Math.Abs(Input.GetAxis("Mouse Y")) > 0;
        }
    }
}