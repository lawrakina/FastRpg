using Manager;
using UnityEngine;
using UnityEngine.AI;


namespace VIew
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(NavMeshAgent))]
    // [RequireComponent(typeof(CharacterController))]
    public abstract class BaseUnitView: MonoBehaviour
    {
        public bool isEnable = true;
        public Transform Transform;
        public NavMeshAgent Agent;
        public Collider Collider;
        public Rigidbody Rigidbody;
        public Animator Animator;
        // public CharacterController CharacterController;
        public AnimatorParammeters AnimatorParams;

        #region fields

        [Header("Movement")]
        #region movenment
        
        public float _moveSpeed = 5.0f;

        // public float _maxMoveSpeed = 5.0f;

        #endregion

        #endregion
        
        
        #region PrivateClass

        public class AnimatorParammeters
        {
            private Animator _animator;

            private bool _battle;
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


            public AnimatorParammeters(Animator animator)
            {
                _animator = animator;
            }
        }

        #endregion
        
        private float _speed;

        public float Speed
        {
            get => _speed;
            set
            {
                _speed = value;
                AnimatorParams.Speed = value;
            }
        }

        protected virtual void Awake()
        {
            Transform = GetComponent<Transform>();
            Rigidbody = GetComponent<Rigidbody>();
            Collider = GetComponent<Collider>();
            Agent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
            AnimatorParams = new AnimatorParammeters(Animator);
            // CharacterController = GetComponent<CharacterController>();
        }
    }
}