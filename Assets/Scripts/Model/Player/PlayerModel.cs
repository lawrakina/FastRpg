using System;
using Controller.TimeRemaining;
using Helper;
using Interface;
using UnityEngine;
using UnityEngine.AI;


namespace Model.Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(BaseUnitAnimationEvents))]
    public sealed class PlayerModel : BaseUnitModel, ICollision
    {
        #region Fields

        public event Action<BaseUnitModel> OnDieChange;

        private ITimeRemaining _enablerNavMeshAgent;
        private float _currentTimeToEnableNavMeshAgent;
        private float _timeToEnableNavMeshAgent;
        private bool _isTimeRemainingEnable;

        #endregion


        #region Properties

        public MovementSystem MovementSystem;
        public VisionSystem VisionSystem;
        public BattleSystem BattleSystem;

        public EquipmentSystem EquipmentSystem;
        // public Weapon Weapon;

        #endregion


        protected override void Start()
        {
            base.Start();
            EquipmentSystem.Constructor(this, ref BattleSystem);
            VisionSystem.Constructor(this, ref BattleSystem, ref MovementSystem, ref EquipmentSystem);
            MovementSystem.Constructor(this,ref VisionSystem,ref BattleSystem);
            BattleSystem.Constructor(this,ref VisionSystem,ref MovementSystem);

            // CashNavMeshAgent.speed = MovementSystem.Speed;
            CashNavMeshAgent.speed = 5f;
            CashNavMeshAgent.angularSpeed = 1000f;
            CashNavMeshAgent.acceleration = 50f;
        }


        public override void Execute()
        {
            base.Execute();
            if(Dbg.LogingEnabled)
                VisionSystem.DrawDebug();
            // VisionSystem.CheckNpc_OLD();
        }

        public void OnCollision(InfoCollision info)
        {
            if (Hp > 0)
            {
                Hp -= info.Damage;
            }

            if (Hp <= 0)
            {
                foreach (var child in GetComponentsInChildren<Transform>())
                {
                    child.parent = null;

                    var tempRbChild = child.GetComponent<Rigidbody>();
                    if (!tempRbChild)
                    {
                        tempRbChild = child.gameObject.AddComponent<Rigidbody>();
                    }

                    Destroy(child.gameObject, 10);
                }

                OnDieChange?.Invoke(this);
            }
        }

        public void Move(Vector3 moveVector)
        {
            if (moveVector.magnitude > 0f)
            {
                MovementSystem.Move(moveVector);
            }
            else
            {
                MovementSystem.AutoMove();
            }
        }

        protected override void StateUnitNormal()
        {
            base.StateUnitNormal();
            EquipmentSystem.SheathWeapon();
        }

        protected override void StateUnitBattle()
        {
            base.StateUnitBattle();
            EquipmentSystem.UnsheathWeapons();
        }
    }
}