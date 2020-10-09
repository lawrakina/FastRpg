using Controller;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace Initializer
{
    public sealed class PlayerInitializator
    {
        public PlayerInitializator(Services services, GameContext gameContext)
        {
            var spawnerPlayer = Object.Instantiate( gameContext.PlayerData.PlayerStruct.StoragePlayer,
                gameContext.PlayerData.PlayerStruct.StartPosition.position,
                gameContext.PlayerData.PlayerStruct.StartPosition.rotation);
            
            PlayerStruct playerStruct = gameContext.PlayerData.PlayerStruct;
            playerStruct.Player = spawnerPlayer;

            var playerModel = new PlayerModel(playerStruct);
            var playerView = new PlayerView(playerStruct);
            
            services.PlayerController = new PlayerController(services, gameContext, playerModel, playerView);
            services.MainController.AddUpdated(services.PlayerController);
        }
    }

    public class PlayerModel
    {
        private PlayerStruct _struct;
        public PlayerModel(PlayerStruct playerStruct)
        {
            _struct = playerStruct;
        }
    }

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class BaseUnitView: MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Collider _collider;
        private Rigidbody _rigidbody;
        
        
        
        #region PrivateClass

        public class AnimatorParammeters
        {
            private Animator _animator;

            private bool _battle;
            private bool _move;
            private bool _falling;
            private bool _jump;
            private bool _attack;
            private int _weaponType;
            private int _attackType;
            private float _speed;

            public bool Battle
            {
                get => _battle;
                set
                {
                    if (_battle && !value)
                        _animator.SetTrigger(TagManager.ANIMATOR_PARAM_WEAPON_UNSHEATH_TRIGGER);
                    if (!_battle && value)
                        _animator.SetTrigger(TagManager.ANIMATOR_PARAM_WEAPON_SHEATH_TRIGGER);
                    _battle = value;
                    _animator.SetBool(TagManager.ANIMATOR_PARAM_BATTLE, value);
                }
            }

            public bool Move
            {
                get => _move;
                set
                {
                    _move = value;
                    _animator.SetBool(TagManager.ANIMATOR_PARAM_MOVE, value);
                }
            }

            public float Speed
            {
                get => _speed;
                set
                {
                    _speed = value;
                    _animator.SetFloat(TagManager.ANIMATOR_PARAM_SPEED, value);
                }
            }

            public bool Falling
            {
                get => _falling;
                set
                {
                    _falling = value;
                    _animator.SetBool(TagManager.ANIMATOR_PARAM_FALLING, value);
                }
            }

            public void SetTriggerJump()
            {
                Jump = true;
            }

            public bool Jump
            {
                get => _jump;
                private set
                {
                    _jump = value;
                    _animator.SetTrigger(TagManager.ANIMATOR_PARAM_JUMP_TRIGGER);
                    _jump = !value;
                }
            }

            public void SetTriggerAttack()
            {
                Attack = true;
            }

            public bool Attack
            {
                get => _attack;
                private set
                {
                    _attack = value;
                    _animator.SetTrigger(TagManager.ANIMATOR_PARAM_ATTACK_TRIGGER);
                    _attack = !value;
                }
            }

            public int WeaponType
            {
                get => _weaponType;
                set
                {
                    _weaponType = value;
                    _animator.SetInteger(TagManager.ANIMATOR_PARAM_WEAPON_TYPE, value);
                }
            }

            public int AttackType
            {
                get => _attackType;
                set
                {
                    _attackType = value;
                    _animator.SetInteger(TagManager.ANIMATOR_PARAM_ATTACK_TYPE, value);
                }
            }

            public void ResetTrigger(string name)
            {
                _animator.ResetTrigger(name);
            }


            public AnimatorParammeters(ref Animator animator)
            {
                _animator = animator;
            }
        }

        #endregion
        
        
        public BaseUnitView(BaseUnitStruct playerStruct)
        {
            
        }
    }
    
    
    public sealed class PlayerView: BaseUnitView
    {
        public PlayerView(PlayerStruct playerStruct)
        {
            
        }
    }
}