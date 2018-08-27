using System;
using Models.Arena;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.AbstractViews
{
    public abstract class BaseItem : EventView
    {
        [SerializeField] protected TextMeshProUGUI NameText;
        [SerializeField] protected TextMeshProUGUI DescriptionText;
        [SerializeField] protected Image ArtworkImage;
        [SerializeField] protected TextMeshProUGUI ManaText;
        [SerializeField] protected TextMeshProUGUI AttackText;
        [SerializeField] protected TextMeshProUGUI HealthText;
        [SerializeField] protected TextMeshProUGUI DefenceText;
        
        [SerializeField] private BattleSide _battleSide;
        
        public BattleSide Side
        {
            get { return _battleSide; }
            set { _battleSide = value; }
        }
        
        [SerializeField] private Transform _parentToReturnTo;
        
        public Transform ParentToReturnTo
        {
            get { return _parentToReturnTo; }
            set { _parentToReturnTo = value; }
        }
        
        [SerializeField] private Transform _placeholderParent;

        public Transform PlaceholderParent
        {
            get { return _placeholderParent; }
            set { _placeholderParent = value; }
        }

        [SerializeField] private Transform _placeholder;

        public Transform Placeholder
        {
            get { return _placeholder; }
            protected set { _placeholder = value; }
        }

        [SerializeField] private Transform _mainParenTransform;

        public Transform MainParenTransform
        {
            protected get { return _mainParenTransform; }
            set { _mainParenTransform = value; }
        }
        
        public int Mana => Convert.ToInt32(ManaText.text);

        protected bool HasDraggable;
    }
}