﻿using UniRx;
using UnityEngine;


namespace Unit.Player
{
    public sealed class PlayerView : MonoBehaviour, IPlayerView
    {
        #region UnityMethods

        private void Awake()
        {
            Transform = GetComponent<Transform>();
            Rigidbody = GetComponent<Rigidbody>();
            Collider = GetComponent<Collider>();
            MeshRenderer = GetComponent<MeshRenderer>();
            _animator = GetComponent<Animator>();
            AnimatorParameters = new AnimatorParameters(ref _animator);

            CharAttributes = new CharAttributes();
        }

        #endregion


        #region Fields

        private Animator _animator;

        #endregion


        #region Properties

        public Transform Transform { get; private set; }

        public Collider Collider { get; private set; }

        public Rigidbody Rigidbody { get; private set; }

        public MeshRenderer MeshRenderer { get; private set; }

        public Animator Animator => _animator;
        public AnimatorParameters AnimatorParameters { get; private set; }

        public ICharAttributes CharAttributes { get; set; }

        public Transform EnemyTarget { get; set; }

        public BaseCharacterClass CharacterClass { get; set; }

        public StringReactiveProperty Description =>
            new StringReactiveProperty(
                $"Name:{CharAttributes.Name}\nClass:{CharacterClass.Name}\nRace:{CharAttributes.CharacterRace}\nGender:{CharAttributes.CharacterGender}");

        #endregion


        #region Events

        // public event Action<InfoCollision> OnBonusUp;

        #endregion


        #region Methods

        // public void OnCollision(InfoCollision infoCollision)
        // {
        //     OnBonusUp?.Invoke(infoCollision);
        // }

        #endregion
    }
}