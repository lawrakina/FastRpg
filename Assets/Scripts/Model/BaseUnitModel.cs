using System;
using Controller.TimeRemaining;
using Enums;
using Helper;
using Interface;
using Manager;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


namespace Model
{
    public abstract class BaseUnitModel : BaseObjectScene, IExecute, IFixedExecute, ISelectObj
    {
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

        #region Fields

        [SerializeField] private string _nameAttachPointLeftHand = "Hand_L";
        [SerializeField] private Vector3 _offsetAttachPointLeftHand = new Vector3(-10f, -2f, 0f);
        private Transform _leftHand;
        [SerializeField] private string _nameAttachPointRightHand = "Hand_R";
        [SerializeField] private Vector3 _offsetAttachPointRightHand = new Vector3(10f, 2f, 0f);
        private Transform _rightHand;
        private Transform[] _gameObjects;

        [SerializeField] protected float _hp = 100;
        [SerializeField] protected float _maxHp = 100;
        private protected StateUnit _state;

        //костыль: нахождение на земле начинаем проверять через пол секунды после получения AddForce
        private float _timeStunned = 0.5f;
        private float _distanceCheckGround = 1.05f;

        #region flags

        //костыль: нахождение на земле начинаем проверять через пол секунды после получения AddForce
        private bool _stunned = false;
        private bool _unstunnedRun = false;

        protected bool IsCharacterController;
        protected bool IsNavMeshAgent;
        protected bool IsKinematicRigidBody;

        #endregion

        [HideInInspector] public CharacterController CashCharacterController;
        [HideInInspector] public NavMeshAgent CashNavMeshAgent;
        [HideInInspector] public bool CashKinematicRigidBody;
        [HideInInspector] public Animator CashAnimator;
        [HideInInspector] public AnimatorParammeters AnimatorParams;
        [HideInInspector] public SoundPlayer SoundPlayer;

        public event Action<BaseUnitModel> OnDieChange;
        
        #endregion


        #region Properties

        public StateUnit StateUnit
        {
            get => _state;
            set => _state = value;
        }

        public float Hp
        {
            get { return _hp; }
            set
            {
                if (value > MaxHp)
                    _hp = MaxHp;
                else
                    _hp = value;
            } //todo добавить расчет коэффициента снижения урона по броне (все закешировать)
        }

        public float MaxHp
        {
            get { return _maxHp; }
        }

        public float PercentXp => Hp; //todo добавить расчет по формуле: выносливость * 17 с занесением в кеш

        #endregion


        #region Methods

        protected virtual void Start()
        {
            CashCharacterController = GetComponent<CharacterController>();
            IsCharacterController = CashCharacterController != null;

            CashNavMeshAgent = GetComponent<NavMeshAgent>();
            IsNavMeshAgent = CashNavMeshAgent != null;

            CashKinematicRigidBody = Rigidbody.isKinematic;
            IsKinematicRigidBody = true;

            CashAnimator = GetComponentInChildren<Animator>();
            AnimatorParams = new AnimatorParammeters(ref CashAnimator);

            SoundPlayer = GetComponentInChildren<SoundPlayer>();

            FindAttachPoints();
        }

        private void FindAttachPoints()
        {
            _gameObjects = GetComponentsInChildren<Transform>();

            for (int i = 0; i < _gameObjects.Length; i++)
            {
                if (_gameObjects[i].name == _nameAttachPointLeftHand)
                {
                    var go = Instantiate(new GameObject(), _gameObjects[i], false);
                    _leftHand = go.transform;
                    _leftHand.name = "_leftHand";
                    _leftHand.localPosition = _offsetAttachPointLeftHand;
                }
                else if (_gameObjects[i].name == _nameAttachPointRightHand)
                {
                    var go = Instantiate(new GameObject(), _gameObjects[i], false);
                    _rightHand = go.transform;
                    _rightHand.name = "_rightHand";
                    _rightHand.localPosition = _offsetAttachPointRightHand;
                }
            }
        }

        #endregion


        public void Bang(InfoCollision collision)
        {
            if (IsCharacterController)
                CashCharacterController.enabled = false;
            if (IsNavMeshAgent)
                CashNavMeshAgent.enabled = false;
            if (IsKinematicRigidBody)
                Rigidbody.isKinematic = !CashKinematicRigidBody;

            Rigidbody.AddForce(collision.Direction, ForceMode.Impulse);

            var tempObj = GetComponent<ICollision>();
            if (tempObj != null)
                tempObj.OnCollision(collision);

            _state = StateUnit.Fly;
        }


        private void UnStunned()
        {
            _stunned = true;
            _unstunnedRun = true;
            var timeRemainingUnStunned = new TimeRemaining(delegate { _stunned = false; }, _timeStunned);
            timeRemainingUnStunned.AddTimeRemainingExecute();
        }

        private void CheckGround()
        {
            if (_stunned) return;
            Debug.DrawRay(Transform.position, Vector3.down, Color.magenta, _distanceCheckGround);
            if (Physics.Raycast(Transform.position, Vector3.down, _distanceCheckGround))
            {
                if (IsCharacterController)
                    CashCharacterController.enabled = true;
                if (IsNavMeshAgent)
                {
                    CashNavMeshAgent.enabled = true;
                }

                if (IsKinematicRigidBody)
                    Rigidbody.isKinematic = CashKinematicRigidBody;
                StateUnit = StateUnit.Idle;

                Debug.Log($"PlayerModel.CheckGround:TRUE");
            }
            else
                Debug.Log($"PlayerModel.CheckGround:FALSE");
        }

        protected bool IsGrounded()
        {
            if (_stunned) return false;
            Debug.DrawRay(Transform.position, Vector3.down, Color.magenta, _distanceCheckGround);
            if (Physics.Raycast(Transform.position, Vector3.down, _distanceCheckGround))
                return true;
            else
                return false;
        }

        public void OnHealing(float delta)
        {
            if (Hp > 0)
            {
                Hp += delta;
            }
        }
        
        protected void SetDamage(InfoCollision info)
        {
            //todo реакциия на попадание  
            if (Hp > 0)
            {
                Hp -= info.Damage;
            }

            if (Hp <= 0)
            {
                StateUnit = StateUnit.Died;
                CashNavMeshAgent.enabled = false;
                foreach (var child in GetComponentsInChildren<Transform>())
                {
                    child.parent = null;

                    var tempRbChild = child.GetComponent<Rigidbody>();
                    if (!tempRbChild)
                    {
                        tempRbChild = child.gameObject.AddComponent<Rigidbody>();
                    }

                    tempRbChild.isKinematic = false;
                    tempRbChild.AddForce(info.Direction * Random.Range(10, 20));

                    Destroy(child.gameObject, 10);
                }

                OnDieChange?.Invoke(this);
            }
        }


        public virtual void Execute()
        {
            switch (StateUnit)
            {
                case StateUnit.Normal:
                    StateUnitNormal();
                    break;
                case StateUnit.Idle:
                    StateUnitIdle();
                    break;
                case StateUnit.Battle:
                    StateUnitBattle();
                    break;
                case StateUnit.Stunned:
                    StateUnitStunned();
                    break;
                case StateUnit.Fly:
                    StateUnitFly();
                    break;
                case StateUnit.Died:
                    StateUnitDied();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public virtual void FixedExecute()
        {
        }

        protected virtual void StateUnitNormal()
        {
            AnimatorParams.Battle = false;
        }

        #region StateUnit

        protected virtual void StateUnitIdle()
        {
            // CashAnimator.SetBool($"Move", false);
        }

        // protected void StateUnitRun()
        // {
        //     CashAnimator.SetBool($"Move", true);
        // }

        protected virtual void StateUnitBattle()
        {
            AnimatorParams.Battle = true;
        }

        protected virtual void StateUnitStunned()
        {
        }

        protected virtual void StateUnitFly()
        {
            if (!_unstunnedRun)
                UnStunned();
            else
                CheckGround();
        }

        protected virtual void StateUnitDied()
        {
        }

        #endregion

        public string GetMessage()
        {
            return $"{gameObject.name}";
        }

        public void ShowBarOnScreen()
        {
        }
    }
}