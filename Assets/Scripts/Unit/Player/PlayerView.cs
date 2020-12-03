using System;
using Model;
using UnityEngine;


namespace Unit.Player
{
    public sealed class PlayerView : MonoBehaviour, IPlayerView
    {
        #region Fields

        private Transform _transform;
        private Collider _collider;
        private Rigidbody _rigidbody;
        private MeshRenderer _meshRenderer;
        private Animator _animator;
        private AnimatorParameters _animatorParameters;

        #endregion


        #region Properties

        public Transform          Transform()                           => _transform;
        public Collider           Collider()                            => _collider;
        public Rigidbody          Rigidbody()                           => _rigidbody;
        public MeshRenderer       MeshRenderer()                        => _meshRenderer;
        public Animator           Animator()                            => _animator;
        public AnimatorParameters AnimatorParameters(Animator animator) => _animatorParameters;

        #endregion


        #region Events

        public event Action<InfoCollision> OnBonusUp;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        #endregion


        #region Methods

        public void OnCollision(InfoCollision infoCollision)
        {
            OnBonusUp?.Invoke(infoCollision);
        }

        #endregion
    }
}