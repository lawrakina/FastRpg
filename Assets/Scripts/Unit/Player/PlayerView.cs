using UniRx;
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
        private Transform _enemyTarget;
        private ICharAttributes _charAttributes;

        #endregion


        #region Properties

        public Transform Transform => _transform;
        public Collider Collider => _collider;
        public Rigidbody Rigidbody => _rigidbody;
        public MeshRenderer MeshRenderer => _meshRenderer;
        public Animator Animator => _animator;
        public AnimatorParameters AnimatorParameters => _animatorParameters;
        public ICharAttributes CharAttributes
        {
            get => _charAttributes;
            set => _charAttributes = value;
        }
        public Transform EnemyTarget
        {
            get => _enemyTarget;
            set => _enemyTarget = value;
        }
        public BaseCharacterClass CharacterClass { get; set; }

        public StringReactiveProperty Description
        {
            get
            {
                return new StringReactiveProperty(
                    $"Name:{CharAttributes.Name}\nClass:{CharacterClass.Name}\nRace:{CharAttributes.CharacterRace}\nGender:{CharAttributes.CharacterGender}");
            }
        }

        #endregion


        #region Events

        // public event Action<InfoCollision> OnBonusUp;

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

            _charAttributes = new CharAttributes();
        }

        #endregion


        #region Methods

        // public void OnCollision(InfoCollision infoCollision)
        // {
        //     OnBonusUp?.Invoke(infoCollision);
        // }

        #endregion
    }
}