using UnityEngine;


namespace Model.Player
{
    [System.Serializable]
    public sealed class MovementSystem
    {
        #region Fields

        private float _speed;
        private float _gravityForce = 0f;
        [SerializeField] private float _moveSpeed = 5f;
        private Vector3 _direction;

        private PlayerModel _owner;
        private VisionSystem _visionSystemSystem;
        private BattleSystem _battleSystem;

        #endregion


        #region Properties

        public float Speed
        {
            get => _speed;
            set
            {
                _speed = value;

                _owner.AnimatorParams.Speed = value;
                
                if (_speed > 0.1f)
                    _owner.AnimatorParams.Move = true;
                else
                    _owner.AnimatorParams.Move = false;
            }
        }

        #endregion


        #region Constructor

        public void Constructor(PlayerModel playerModel, ref VisionSystem visionSystem, ref
            BattleSystem battleSystem)
        {
            _owner = playerModel;
            _visionSystemSystem = visionSystem;
            _battleSystem = battleSystem;
        }

        #endregion


        #region Methods

        public void AutoMove()
        {
            if (_visionSystemSystem.Target != null && _battleSystem.IsAutoAttack)
            {
                _owner.CashNavMeshAgent.enabled = true;
                _owner.CashNavMeshAgent.stoppingDistance =
                    _owner.EquipmentSystem.AttackingWeapon.RecommendedAttackDistance;
                _owner.CashNavMeshAgent.SetDestination(_visionSystemSystem.Target.Transform.position);
            }

            Speed = _owner.CashNavMeshAgent.velocity.magnitude;
        }

        public void Move(Vector3 moveVector)
        {
            _owner.CashNavMeshAgent.enabled = false;
            Speed = moveVector.magnitude * _moveSpeed;
            // if (CashCharacterController.isGrounded)
            // {
            // CashAnimator.ResetTrigger($"Jump");
            // CashAnimator.SetBool($"Falling", false);


            // if (Math.Abs(moveVector.x) > _movementTolerance || Math.Abs(moveVector.z) > _movementTolerance)
            //     StateUnit = StateUnit.Run;
            // else
            //     StateUnit = StateUnit.Idle;

            if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
            {
                _direction = Vector3.RotateTowards(_owner.Transform.forward, moveVector, _moveSpeed, 0.0f);
                _owner.Transform.rotation = Quaternion.LookRotation(new Vector3(_direction.x, 0f, _direction.z));
            }

            GamingGravity();

            if (!_owner.CashAnimator.applyRootMotion)
            {
                moveVector.y = _gravityForce;
                _owner.CashCharacterController.Move(moveVector * (Time.deltaTime * _moveSpeed));
            }
            else
            {
                moveVector = new Vector3(0f, _gravityForce, 0f);
                _owner.CashCharacterController.Move(moveVector * (Time.deltaTime * _moveSpeed));
            }

            // }
            // else
            // {
            //     if(_gravityForce < -3f)
            //         CashAnimator.SetBool($"Falling", true);
            // }
        }

        private void GamingGravity()
        {
            if (!_owner.CashCharacterController.isGrounded)
                _gravityForce -= 20f * Time.deltaTime;
            else
                _gravityForce = -1f;
        }

        #endregion

        public void RotationToTarget(BaseUnitModel target)
        {
            var vector = new Vector3(
                target.Transform.position.x,
                _owner.Transform.position.y,
                target.Transform.position.z);
            _owner.Transform.LookAt(vector);
        }
    }
}