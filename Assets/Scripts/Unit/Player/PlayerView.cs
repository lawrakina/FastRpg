using System;
using Data;
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
        [SerializeField] private float _speed;
        private Transform _enemyTarget;
        [SerializeField] private float _agroDistance;
        [SerializeField] private float _rotateSpeedPlayer;
        private CharacterSettings _playerSetting;

        #endregion


        #region Properties

        public Transform          Transform()          => _transform;
        public Collider           Collider()           => _collider;
        public Rigidbody          Rigidbody()          => _rigidbody;
        public MeshRenderer       MeshRenderer()       => _meshRenderer;
        public Animator           Animator()           => _animator;
        public AnimatorParameters AnimatorParameters() => _animatorParameters;

        public Transform EnemyTarget
        {
            get => _enemyTarget;
            set => _enemyTarget = value;
        }

        public float AgroDistance
        {
            get => _agroDistance;
            set => _agroDistance = value;
        }
        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        public float RotateSpeedPlayer
        {
            get => _rotateSpeedPlayer;
            set => _rotateSpeedPlayer = value;
        }

        public CharacterSettings PlayerSettings
        {
            get => _playerSetting;
            set => _playerSetting = value;
        }

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
            _animator = GetComponent<Animator>();
            _animatorParameters = new AnimatorParameters(ref _animator);
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