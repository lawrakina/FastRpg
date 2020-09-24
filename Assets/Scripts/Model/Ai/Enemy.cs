using System;
using Controller.TimeRemaining;
using Enums;
using Helper;
using Model.Weapons;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Model.Ai
{
    public class Enemy : BaseUnitModel
    {
        #region Properties

        public Vision Vision;
        public Weapon _weapon; //todo с разным оружием 
        public Transform Target { get; set; }
        public NavMeshAgent Agent { get; private set; }
        public StateEnemy StateEnemy;
        private float _waitTime = 3;
        private Vector3 _point;
        private float _stoppingDistance = 10.0f;
        private float _patrolStoppingDistance = 1.0f;
        private float _deltaTimeMoving = 0.0f;
        private float _timeToMoving = 5.0f;

        public event Action<Enemy> OnDieChange;
        private ITimeRemaining _timerResetState;
        [SerializeField] private LayerMask _enemyLayerMask;

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            Agent = GetComponent<NavMeshAgent>();
            Agent.stoppingDistance = _stoppingDistance;
            _timerResetState = new TimeRemaining(ResetStateBot, _waitTime);
            _weapon.enabled = true;
            StateUnit = StateUnit.Idle;
            StateEnemy = StateEnemy.Patrol;
        }

        private void OnEnable()
        {
            var bodyBot = GetComponentInChildren<BodyBot>();
            if (bodyBot != null) bodyBot.OnApplyDamageChange += SetDamage;

            var headBot = GetComponentInChildren<HeadBot>();
            if (headBot != null) headBot.OnApplyDamageChange += SetDamage;
        }

        private void OnDisable()
        {
            var bodyBot = GetComponentInChildren<BodyBot>();
            if (bodyBot != null) bodyBot.OnApplyDamageChange -= SetDamage;

            var headBot = GetComponentInChildren<HeadBot>();
            if (headBot != null) headBot.OnApplyDamageChange -= SetDamage;
        }

        #endregion


        #region IExecute

        public override void Execute()
        {
            Dbg.Log($"{name}: StateUnit:{StateUnit}, StateEnemy:{StateEnemy}");
            base.Execute();
            if (StateUnit == StateUnit.Died) return;

            if (StateUnit == StateUnit.Idle)
            {
                //виден противник? Да -> в бой
                if (Vision.VisionM(transform, Target))
                    StateUnitBattle();

                if (StateEnemy != StateEnemy.Static)
                {
                    if (!Agent.hasPath)
                    {
                        if (StateEnemy == StateEnemy.Inspection)
                        {
                            StateEnemy = StateEnemy.Patrol;
                            _point = Patrol.GenericPoint(transform);//todo сделать генерацию валидной точки
                            Agent.stoppingDistance = _patrolStoppingDistance;
                        }
                        else
                        {
                            if ((_point - transform.position).sqrMagnitude <= 1)
                            {
                                StateEnemy = StateEnemy.Inspection;
                                _timerResetState.AddTimeRemainingExecute();
                            }
                        }
                    }
                }
            }
            else
            {
                if (Math.Abs(Agent.stoppingDistance - _stoppingDistance) > Mathf.Epsilon)
                {
                    // Debug.Log($"Agent.stoppingDistance {Agent.stoppingDistance}");
                    Agent.stoppingDistance = _stoppingDistance;
                }

                if (Vision.VisionM(transform, Target))
                {
                    // Debug.Log($"Weapon.Fire(); {transform.name}, {Target.name}");
                    Transform.LookAt(Target);
                    _weapon.Attack(Transform.position, Vector3.forward, _enemyLayerMask );
                }
                else
                {
                    MovePoint(Target.position);

                    if (_deltaTimeMoving > _timeToMoving)
                    {
                        _deltaTimeMoving = 0.0f;

                        StateEnemy = StateEnemy.Patrol;
                        _point = Patrol.GenericPoint(transform);
                        MovePoint(_point);
                        Agent.stoppingDistance = _patrolStoppingDistance;
                    }
                    else
                    {
                        _deltaTimeMoving += Time.deltaTime;
                    }
                }
            }
        }

        #endregion


        #region Methods

        #region StateUnit

        protected override void StateUnitNormal()
        {
            base.StateUnitNormal();
            Color = Color.white;
        }

        protected override void StateUnitIdle()
        {
            base.StateUnitIdle();
            Color = Color.yellow;
        }

        protected override void StateUnitBattle()
        {
            base.StateUnitBattle();
            Color = Color.red;
        }

        protected override void StateUnitDied()
        {
            base.StateUnitDied();
            Color = Color.gray;
        }

        protected override void StateUnitFly()
        {
            base.StateUnitFly();
            Color = Color.blue;
        }

        protected override void StateUnitStunned()
        {
            base.StateUnitStunned();
            Color = Color.magenta;
        }

        #endregion
        
        public void MovePoint(Vector3 point)
        {
            if (Agent.enabled)
                Agent.SetDestination(point);
        }

        private void ResetStateBot()
        {
            StateUnitIdle();
        }

        private void SetDamage(InfoCollision info)
        {
            //todo реакциия на попадание  
            if (Hp > 0)
            {
                Hp -= info.Damage;
                StateUnitBattle();
            }

            if (Hp <= 0)
            {
                StateUnitDied();
                Agent.enabled = false;
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

        #endregion
    }
}