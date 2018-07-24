using System;
using DG.Tweening;
using Models.Arena;
using UnityEngine;
using UnityEngine.EventSystems;

namespace View.DeckItems
{
    public class TrateView : DraggableView
    {
        [SerializeField] private BattleTrate _trate;
        
        private readonly Vector3[] _waypoints = new Vector3[2];

        /// <summary>
        /// On add trate to card
        /// </summary>
        public event Action<TrateView> OnStartDrag;

        public BattleTrate Trate
        {
            get { return _trate; }
            private set { _trate = value; }
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

        /// <inheritdoc />
        /// <summary>
        /// On begin drag
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnBeginDrag(PointerEventData eventData)
        {
            OnStartDrag?.Invoke(this);
            base.OnBeginDrag(eventData);
        }
        
        
        /// <summary>
        /// Start path animation
        /// </summary>
        public void StartPathAnimation()
        {
            _waypoints[0] = PlaceholderParent.position;
            _waypoints[1] = Placeholder.position;
            var path = transform.DOPath(_waypoints, 1, PathType.CatmullRom, PathMode.TopDown2D)
                .SetOptions(false, AxisConstraint.Z);
            path.onPlay += OnStartPathAnimation;
            path.onComplete += OnCompletePathAnimation;
        }
        
        /// <summary>
        /// On complete animation
        /// </summary>
        private void OnCompletePathAnimation()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            transform.SetParent(PlaceholderParent);
            transform.SetSiblingIndex(Placeholder.GetSiblingIndex());
            Destroy(Placeholder.gameObject);
        }

        /// <summary>
        /// On start animation
        /// </summary>
        private void OnStartPathAnimation()
        {
            transform.SetParent(MainParenTransform);
        }
    }
}