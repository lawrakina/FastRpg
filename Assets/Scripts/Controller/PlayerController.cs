using Interface;
using Model;
using UnityEngine;
using VIew;


namespace Controller
{
    public sealed class PlayerController : IUpdated, IFixedUpdate, IEnabled, ILateUpdated
    {
        #region Fields

        private Services _services;
        private GameContext _context;
        private PlayerView _playerView;
        private BaseUnitModel _model;
        private HealthBarView _healthBarView;

        #endregion


        #region tempFieds

        private Vector3 _direction;
        private float _gravityForce;

        #endregion


        #region ctor

        public PlayerController(
            Services services, GameContext context, 
            BaseUnitModel playerModel,
            PlayerView playerView, 
            HealthBarView healthBarView)
        {
            _services = services;
            _context = context;
            _playerView = playerView;
            _model = playerModel;
            _healthBarView = healthBarView;
        }

        #endregion


        #region IEnabled

        public void On()
        {
            _healthBarView.On();
        }

        public void Off()
        {
            _healthBarView.Off();
        }

        #endregion

        
        #region IUdpatable,ILateUpdate,IFixedUpdate

        public void LateUpdateTick()
        {
            HealthAlignCamera();
        }

        public void UpdateTick()
        {
            GamingGravity();
            UpdateHealthBar();
        }
        
        public void FixedUpdateTick()
        {
            Move();
        }

        #endregion


        #region PublicMethods

        public void Move(Vector3 moveVector)
        {
            _direction = Vector3.ClampMagnitude(moveVector, 1f);


            _playerView.Speed = _direction.magnitude * _playerView._moveSpeed;
            if (moveVector.sqrMagnitude < 0.05f) return;

            if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
            {
                _playerView.Transform.rotation = Quaternion.LookRotation(new Vector3(_direction.x, 0f, _direction.z));
            }
        }

        #endregion

        
        #region PrivateMethods

        private void Move()
        {
            var movingVector = new Vector3(_direction.x, 0f, _direction.z);
            _playerView.Rigidbody.MovePosition(_playerView.Transform.position +
                                               movingVector * (_playerView._moveSpeed * Time.fixedDeltaTime));
            _playerView.Rigidbody.AddForce(new Vector3(0f, _gravityForce * Time.fixedDeltaTime, 0f), ForceMode.Impulse);
        }
        
        private void GamingGravity()
        {
            if (!IsGrounded)
                _gravityForce -= 2f;
            else
                _gravityForce = -1f;
            
            _direction.y = _gravityForce;
        }

        private bool IsGrounded
        {
            get
            {
                // RaycastHit hit;
                // Debug.DrawRay(_player.transform.position + new Vector3(0.0f, 0.5f, 0.0f), Vector3.down, Color.red,
                // _player.distanceToCheckGround);
                return Physics.Raycast(_playerView.transform.position + Vector3.up/2, Vector3.down, out _,
                    _playerView.distanceToCheckGround, _context.GroundLayer);
            }
        }
        
        private void UpdateHealthBar()
        {
            if (_model.Hp <= _model.MaxHp) {
                _healthBarView.MeshRenderer.enabled = true;
                _healthBarView.UpdateParams(_model.Hp, _model.MaxHp);
            } else {
                _healthBarView.MeshRenderer.enabled = false;
            }
        }

        private void HealthAlignCamera()
        {
            if (_model.Hp <= _model.MaxHp)
            {
                _healthBarView.AlignCamera(_services.ThirdCameraController.CameraTransform);
            }
        }

        #endregion

        #region NonUsed NonRemoved

        //можно использовать для прыжков 
        private bool _isGroundedForJump
        {
            get
            {
                var playerCollider = _playerView.Collider.bounds;

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
            _playerView.Agent.enabled = false;

            moveVector = Vector3.ClampMagnitude(moveVector, 1f);

            if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
            {
                _direction = Vector3.RotateTowards(_playerView.Transform.forward, moveVector, _playerView._moveSpeed, 0.0f);
                _playerView.Transform.rotation = Quaternion.LookRotation(new Vector3(_direction.x, 0f, _direction.z));
            }

            // GamingGravity();
            if (!_playerView.Animator.applyRootMotion)
                moveVector.y = _gravityForce;
            else
                moveVector = new Vector3(0f, _gravityForce, 0f);

            // _player.CharacterController.Move(moveVector * (Time.deltaTime * _player._moveSpeed));
            // _player.Rigidbody.AddForce(moveVector * (_player._moveSpeed), ForceMode.Impulse);
            // if (_player.Rigidbody.velocity.magnitude > _player._maxMoveSpeed)
            //     _player.Rigidbody.velocity = Vector3.ClampMagnitude(moveVector, 1f) * _player._maxMoveSpeed;
            // _player.Speed = _player.CharacterController.velocity.magnitude;
        }


        #endregion
    }
}