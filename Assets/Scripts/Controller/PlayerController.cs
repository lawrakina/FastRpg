using Model;
using UnityEngine;
using VIew;


namespace Controller
{
    public sealed class EnemyController : BaseUnitController
    {
        //todo добавиьб машину состояний в модель
        
        public EnemyController(Services services, GameContext context, BaseUnitModel baseUnitModel, PlayerView baseUnitView,
            HealthBarView healthBarView) : base(services, context, baseUnitModel, baseUnitView, healthBarView)
        {
        }

        public override void UpdateTick()
        {
            base.UpdateTick();
            
        }

        public override void Move(Vector3 moveVector)
        {
        }

        protected override void Move()
        {
        }
    }

    public sealed class PlayerController : BaseUnitController
    {
        #region ctor

        public PlayerController(
            Services services, GameContext context,
            BaseUnitModel baseUnitModel,
            PlayerView playerUnitView,
            HealthBarView healthBarView
        ) : base(services, context, baseUnitModel, playerUnitView, healthBarView)
        {
        }

        #endregion


        #region IEnabled

        public override void On()
        {
            base.On();
        }

        public override void Off()
        {
            base.Off();
        }

        #endregion


        #region IUdpatable,ILateUpdate,IFixedUpdate

        public override void LateUpdateTick()
        {
            base.LateUpdateTick();
        }

        public override void UpdateTick()
        {
            base.UpdateTick();
        }

        public override void FixedUpdateTick()
        {
            base.FixedUpdateTick();
        }

        #endregion


        #region Move

        public override void Move(Vector3 moveVector)
        {
            Direction = Vector3.ClampMagnitude(moveVector, 1f);


            _baseUnitView.Speed = Direction.magnitude * _baseUnitView._moveSpeed;
            if (moveVector.sqrMagnitude < 0.05f) return;

            if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
            {
                _baseUnitView.Transform.rotation = Quaternion.LookRotation(new Vector3(Direction.x, 0f, Direction.z));
            }
        }

        protected override void Move()
        {
            var movingVector = new Vector3(Direction.x, 0f, Direction.z);
            _baseUnitView.Rigidbody.MovePosition(_baseUnitView.Transform.position +
                                               movingVector * (_baseUnitView._moveSpeed * Time.fixedDeltaTime));
            _baseUnitView.Rigidbody.AddForce(new Vector3(0f, GravityForce * Time.fixedDeltaTime, 0f), ForceMode.Impulse);
        }

        #endregion


        #region NonUsed NonRemoved

        //можно использовать для прыжков 
        private bool _isGroundedForJump
        {
            get
            {
                var playerCollider = _baseUnitView.Collider.bounds;

                var bottomCenterPoint = new Vector3(playerCollider.center.x, playerCollider.min.y,
                    playerCollider.center.z);

                //создаем невидимую физическую капсулу и проверяем не пересекает ли она обьект который относится к полу

                //_collider.bounds.size.x / 2 * 0.9f -- эта странная конструкция берет радиус обьекта.
                // был бы обязательно сферой -- брался бы радиус напрямую, а так пишем по-универсальнее

                DebugExtension.DebugCapsule(playerCollider.center, bottomCenterPoint,
                    playerCollider.size.x / 2 * 0.9f);
                return Physics.CheckCapsule(playerCollider.center, bottomCenterPoint,
                    playerCollider.size.x / 2 * 0.9f, _context.GroundLayer);
                // если можно будет прыгать в воздухе, то нужно будет изменить коэфициент 0.9 на меньший.
            }
        }


        public void MoveOld(Vector3 moveVector)
        {
            _baseUnitView.Agent.enabled = false;

            moveVector = Vector3.ClampMagnitude(moveVector, 1f);

            if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
            {
                Direction = Vector3.RotateTowards(_baseUnitView.Transform.forward, moveVector, _baseUnitView._moveSpeed,
                    0.0f);
                _baseUnitView.Transform.rotation = Quaternion.LookRotation(new Vector3(Direction.x, 0f, Direction.z));
            }

            // GamingGravity();
            if (!_baseUnitView.Animator.applyRootMotion)
                moveVector.y = GravityForce;
            else
                moveVector = new Vector3(0f, GravityForce, 0f);

            // _player.CharacterController.Move(moveVector * (Time.deltaTime * _player._moveSpeed));
            // _player.Rigidbody.AddForce(moveVector * (_player._moveSpeed), ForceMode.Impulse);
            // if (_player.Rigidbody.velocity.magnitude > _player._maxMoveSpeed)
            //     _player.Rigidbody.velocity = Vector3.ClampMagnitude(moveVector, 1f) * _player._maxMoveSpeed;
            // _player.Speed = _player.CharacterController.velocity.magnitude;
        }

        #endregion
    }
}