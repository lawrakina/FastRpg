using Extension;
using UnityEngine;


namespace Unit
{
    public sealed class AnimatorParameters
    {
        private Animator _animator;

        private bool _battle;
        private bool _falling;
        private bool _jump;
        private bool _attack;
        private int _weaponType;
        private int _attackType;
        private float _move;

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

        public float Move
        {
            get => _move;
            set
            {
                _move = value;
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


        public AnimatorParameters(ref Animator animator)
        {
            _animator = animator;
        }
    }
}