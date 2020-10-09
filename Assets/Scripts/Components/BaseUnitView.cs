﻿using Manager;
using UnityEngine;
using UnityEngine.AI;


namespace Components
{
    public abstract class BaseUnitView: MonoBehaviour
    {
        protected NavMeshAgent _agent;
        protected Collider _collider;
        protected Rigidbody _rigidbody;
        protected Animation _animation;

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
        
    }
}