using Initializator;
using Interface;
using UnityEngine;
using VIew;


namespace Controller
{
    public sealed class PlayerController : IUpdated, IFixedUpdate
    {
        #region Fields

        private Services _services;
        private GameContext _context;
        private PlayerView _player;

        #endregion


        #region tempFieds

        private Vector3 _direction;
        private float _gravityForce;

        #endregion


        public PlayerController(Services services, GameContext context, PlayerView playerView)
        {
            _services = services;
            _context = context;
            _player = playerView;
        }


        #region IUdpatable

        public void UpdateTick()
        {
            GamingGravity();
            _direction.y = _gravityForce;
        }

        #endregion


        #region IFixedUpdate

        public void FixedUpdateTick()
        {
            var _movingVector = new Vector3(_direction.x, 0f, _direction.z);
            _player.Rigidbody.MovePosition(_player.Transform.position +
                                           _movingVector * (_player._moveSpeed * Time.fixedDeltaTime));
            _player.Rigidbody.AddForce(new Vector3(0f,_gravityForce * Time.fixedDeltaTime,0f), ForceMode.Impulse);
        }

        #endregion

        public void Move(Vector3 moveVector)
        {
            _direction = Vector3.ClampMagnitude(moveVector, 1f);


            // _player.Speed = _player.Rigidbody.velocity.magnitude;
            _player.Speed = _direction.magnitude * _player._moveSpeed;
            if (moveVector.sqrMagnitude < 0.05f) return;

            if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
            {
                // _direction = Vector3.RotateTowards(_player.Transform.forward, moveVector, _player._moveSpeed, 0.0f);
                _player.Transform.rotation = Quaternion.LookRotation(new Vector3(_direction.x, 0f, _direction.z));
            }
        }

        public void MoveOld(Vector3 moveVector)
        {
            _player.Agent.enabled = false;

            moveVector = Vector3.ClampMagnitude(moveVector, 1f);

            if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
            {
                _direction = Vector3.RotateTowards(_player.Transform.forward, moveVector, _player._moveSpeed, 0.0f);
                _player.Transform.rotation = Quaternion.LookRotation(new Vector3(_direction.x, 0f, _direction.z));
            }

            // GamingGravity();
            if (!_player.Animator.applyRootMotion)
                moveVector.y = _gravityForce;
            else
                moveVector = new Vector3(0f, _gravityForce, 0f);

            // _player.CharacterController.Move(moveVector * (Time.deltaTime * _player._moveSpeed));
            // _player.Rigidbody.AddForce(moveVector * (_player._moveSpeed), ForceMode.Impulse);
            // if (_player.Rigidbody.velocity.magnitude > _player._maxMoveSpeed)
            //     _player.Rigidbody.velocity = Vector3.ClampMagnitude(moveVector, 1f) * _player._maxMoveSpeed;
            // _player.Speed = _player.CharacterController.velocity.magnitude;
        }

        private void GamingGravity()
        {
            // if (!_player.CharacterController.isGrounded)
            if (!_myIsGrounded)
                _gravityForce -= 2f;
            else
                _gravityForce = -1f;
        }

        private bool _myIsGrounded
        {
            get
            {
                RaycastHit hit;
                Debug.DrawRay(_player.transform.position + new Vector3(0.0f, 0.5f, 0.0f), Vector3.down, Color.red,
                    _player.distanceToCheckGround);
                if (Physics.Raycast(_player.transform.position + new Vector3(0.0f, 0.5f, 0.0f), Vector3.down, out hit,
                    _player.distanceToCheckGround, _context.GroundLayer)
                )
                {
                    Debug.Log($"RayTrue;");
                    return true;
                }
                else
                {
                    Debug.Log($"RayFalse");
                    return false;
                }
            }
        }

        private bool _isGrounded
        {
            get
            {
                var playerCollider = _player.Collider.bounds;

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
    }
}