using Initializator;
using Interface;
using UnityEngine;
using VIew;


namespace Controller
{
    public sealed class PlayerController: IUpdated
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
            
        }

        #endregion

        public void Move(Vector3 moveVector)
        {
            // Debug.Log($"MoveVector:{moveVector}");
            _player.Agent.enabled = false;
            // _player.Speed = moveVector.magnitude;
            _player.Speed = Vector3.ClampMagnitude(moveVector, 1f).magnitude *_player._moveSpeed;

            // Debug.Log($"ClampMagnitude:{Vector3.ClampMagnitude( moveVector, 1f)}");//правильное произведение векторов!
            if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
            {
                _direction = Vector3.RotateTowards(_player.Transform.forward, moveVector, _player._moveSpeed, 0.0f);
                _player.Transform.rotation = Quaternion.LookRotation(new Vector3(_direction.x, 0f, _direction.z));
            }
            
            GamingGravity();
            
            if (!_player.Animator.applyRootMotion)
            {
                moveVector.y = _gravityForce;
                _player.CharacterController.Move(moveVector * (Time.deltaTime * _player._moveSpeed));
            }
            else
            {
                moveVector = new Vector3(0f, _gravityForce, 0f);
                _player.CharacterController.Move(moveVector * (Time.deltaTime * _player._moveSpeed));
            }
        }

        private void GamingGravity()
        {
            if (!_player.CharacterController.isGrounded)
                _gravityForce -= 20f * Time.deltaTime;
            else
                _gravityForce = -1f;
        }
    }
}