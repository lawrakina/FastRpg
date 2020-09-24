using System.Collections.Generic;
using Controller.TimeRemaining;
using Enums;
using Helper;
using UnityEngine;


namespace Model.Player
{
    [System.Serializable]
    public sealed class VisionSystem
    {
        #region Fields

        #region cashFields

        // private static int _maxVisibleColliders = 20;
        // private static int _maxBattleColliders = 20;
        // private static int _maxAttackColliders = 5;
        // private int _countVisibleColliders;
        // private int _countBattleColliders;
        // private int _countAttackColliders;
        // private Collider[] _hitVisibleColliders = new Collider[_maxVisibleColliders];
        // private Collider[] _hitBattleColliders = new Collider[_maxBattleColliders];
        // private Collider[] _hitAttackColliders = new Collider[_maxAttackColliders];
        private Vector3 _position;

        private GameObject[] _enemies;
        private GameObject _closestEnemy;

        #endregion

        // public float _visibleDistance = 20f;
        public float _battleDistance = 15f;
        [HideInInspector] public float SqrDistanceToTarget;
        [HideInInspector] public BaseUnitModel Target;
        [SerializeField] private float _updatePeriodVisibility = 1.0f;

        // public float _activeDis = 10f;
        // public float _activeAng = 90f;

        [SerializeField] public LayerMask EnemiesLayerMask;

        private ITimeRemaining _timerUpdateListEnemies;

        #endregion


        #region Properties

        private PlayerModel _owner;
        private BattleSystem _battleSystem;
        private MovementSystem _movementSystem;
        private EquipmentSystem _equipmentSystem;

        #endregion


        #region Constructor

        public void Constructor(PlayerModel playerModel, ref BattleSystem battleSystem,
            ref MovementSystem movementSystem, ref EquipmentSystem equipmentSystem)
        {
            _owner = playerModel;
            _battleSystem = battleSystem;
            _movementSystem = movementSystem;
            _equipmentSystem = equipmentSystem;

            _timerUpdateListEnemies = new TimeRemaining(CheckNpc, _updatePeriodVisibility, true);
            _timerUpdateListEnemies.AddTimeRemainingExecute();
        }

        #endregion


        #region Methods

        public void CheckNpc()
        {
            _position = _owner.Transform.position;

            //если автоатака включена
            if (_battleSystem.IsAutoAttack)
            {
                //если в дистанции боя кто-то есть то бьем\бежим
                if (Physics.CheckSphere(_position, _battleDistance, EnemiesLayerMask))
                {
                    EnemyDetected();
                    Target = FindClosest();
                    if (SqrDistanceToTarget <= _equipmentSystem.AttackingWeapon.MaxAttackDistance *
                        _equipmentSystem.AttackingWeapon.MaxAttackDistance)
                    {
                        _battleSystem.Attack(Target);
                    }

                    if (SqrDistanceToTarget < _equipmentSystem.AttackingWeapon.RecommendedAttackDistance *
                        _equipmentSystem.AttackingWeapon.RecommendedAttackDistance)
                    {
                        _movementSystem.AutoMove();
                    }
                }
            }
            else
            {
                //если автоатака выключена и кто-то есть на дистанции удара то бьем\бежим
                if (Physics.CheckSphere(_position, _equipmentSystem.AttackingWeapon.MaxAttackDistance,
                    EnemiesLayerMask))
                {
                    EnemyDetected();
                    Target = FindClosest();

                    if (SqrDistanceToTarget <= _equipmentSystem.AttackingWeapon.MaxAttackDistance *
                        _equipmentSystem.AttackingWeapon.MaxAttackDistance)
                    {
                        _battleSystem.Attack(Target);
                    }

                    if (SqrDistanceToTarget < _equipmentSystem.AttackingWeapon.RecommendedAttackDistance *
                        _equipmentSystem.AttackingWeapon.RecommendedAttackDistance)
                    {
                        _movementSystem.AutoMove();
                    }
                }
                else
                {
                    Target = null;
                    EnemyNoDetected();
                }
            }
        }

        private BaseUnitModel FindClosest()
        {
            _enemies = GameObject.FindGameObjectsWithTag("Enemy");
            var distance = Mathf.Infinity;
            foreach (var go in _enemies)
            {
                var diff = go.transform.position - _position;
                var curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    _closestEnemy = go;
                    distance = curDistance;
                    SqrDistanceToTarget = curDistance;
                }
            }

            return _closestEnemy.GetComponent<BaseUnitModel>();
        }

        //старый неоптимизированный код
        // public void CheckNpc_OLD()
        // {
        //     _position = _owner.Transform.position;
        //
        //
        //     //Attack
        //     _countAttackColliders = Physics.OverlapSphereNonAlloc(
        //         _position,
        //         _owner.EquipmentSystem.AttackingWeapon.MaxAttackDistance,
        //         _hitAttackColliders,
        //         EnemiesLayerMask);
        //     Dbg.DebugWireSphere(_position, Color.black,
        //         _owner.EquipmentSystem.AttackingWeapon.MaxAttackDistance);
        //     if (_countAttackColliders > 0)
        //     {
        //         Target = SortingByRange(_hitAttackColliders, _countAttackColliders)[0].GetComponent<BaseUnitModel>();
        //         SqrDistanceToTarget = Distance(_position, Target.transform.position);
        //
        //         // Dbg.Log($"DistanceToTarget{DistanceToTarget}, RecommendedAttackDistance{_equipmentSystem.AttackingWeapon.RecommendedAttackDistance}");
        //
        //         if (SqrDistanceToTarget <= _equipmentSystem.AttackingWeapon.MaxAttackDistance)
        //         {
        //             _battleSystem.Attack(Target);
        //         }
        //
        //         if (SqrDistanceToTarget < _equipmentSystem.AttackingWeapon.RecommendedAttackDistance)
        //         {
        //             _movementSystem.AutoMove();
        //         }
        //
        //         //
        //         // if (DistanceToTarget < _equipmentSystem.AttackingWeapon.RecommendedAttackDistance)
        //         // {
        //         //     Dbg.Log($"VisionSystem.CheckNpc(Attack):  _battleSystem.Attack(Target)");
        //         //     _battleSystem.Attack(Target);
        //         // }
        //         // else
        //         // {
        //         //     Dbg.Log($"VisionSystem.CheckNpc(Attack):  _movementSystem.Move(Target.transform.position)");
        //         //     // _movementSystem.Move(Target.transform.position);
        //         //     _movementSystem.AutoMove();
        //         // }
        //
        //         return;
        //     }
        //
        //     //Battle
        //     _countBattleColliders = Physics.OverlapSphereNonAlloc(
        //         _position,
        //         _battleDistance,
        //         _hitBattleColliders,
        //         EnemiesLayerMask);
        //     Dbg.DebugWireSphere(_position, Color.red, _battleDistance);
        //     if (_countBattleColliders > 0)
        //     {
        //         Dbg.Log($"PlayerModel.DetectEnemy:{_countBattleColliders}");
        //         EnemyDetected();
        //         _battleSystem.IsAutoAttack = true;
        //         Target = SortingByRange(_hitBattleColliders, _countBattleColliders)[0].GetComponent<BaseUnitModel>();
        //         // _movementSystem.Move(Target.transform.position);
        //
        //         return;
        //     }
        //     else
        //     {
        //         _battleSystem.IsAutoAttack = false;
        //     }
        //
        //     //Visible
        //     _countVisibleColliders = Physics.OverlapSphereNonAlloc(
        //         _position,
        //         _visibleDistance,
        //         _hitVisibleColliders,
        //         EnemiesLayerMask);
        //     Dbg.DebugWireSphere(_position, Color.yellow, _visibleDistance);
        //     if (_countVisibleColliders > 0)
        //     {
        //         Dbg.Log($"PlayerModel.DetectNpc:{_countVisibleColliders}");
        //         if (_battleSystem.IsAutoAttack)
        //         {
        //             EnemyDetected();
        //
        //             Target = SortingByRange(_hitVisibleColliders, _countVisibleColliders)[0]
        //                 .GetComponent<BaseUnitModel>();
        //             _movementSystem.Move(Target.transform.position);
        //         }
        //
        //         return;
        //     }
        //     else
        //     {
        //         Target = null;
        //     }
        //
        //     EnemyNoDetected();
        //     Target = null;
        //     //
        //     // //в радиусе атаки
        //     // if (_visionSystem.DistanceToTarget <= _visionSystem._attackDistance)
        //     // {
        //     //     if (Target != null)
        //     //         Battle.Attack(Target, _visionSystem.DistanceToTarget);
        //     // }
        //     // else
        //     //     //в радиусе готовности боя
        //     // if (_visionSystem.DistanceToTarget <= _visionSystem._battleDistance)
        //     // {
        //     //     StateUnit = StateUnit.Battle;
        //     //
        //     //     if (Target == null) return;
        //     //     if (Movement.InputMoveVector == Vector3.zero)
        //     //     {
        //     //         CashNavMeshAgent.Move(Target.Transform.position);
        //     //     }
        //     // }
        //     // else
        //     //     //в радиусе видимости
        //     // if (_visionSystem.DistanceToTarget <= _visionSystem._visibleDistance)
        //     // {
        //     //     StateUnit = StateUnit.Idle;
        //     //
        //     //     if (Movement.InputMoveVector == Vector3.zero)
        //     //     {
        //     //         CashNavMeshAgent.Move(Target.Transform.position);
        //     //     }
        //     // }
        //     // else
        //     // {
        //     //     Target = null;
        //     // }
        //
        //
        //     for (var i = 0; i < _countVisibleColliders; i++)
        //     {
        //         // _hitVisibleColliders[i].SendMessage("ShowBarOnScreen");
        //     }
        //
        //     for (var i = 0; i < _countBattleColliders; i++)
        //     {
        //         // _hitBattleColliders[i].SendMessage("ShowBarOnScreen");
        //     }
        //
        //     for (var i = 0; i < _countAttackColliders; i++)
        //     {
        //         // _hitAttackColliders[i].SendMessage("ShowBarOnScreen");
        //     }
        // }


        public void EnemyDetected()
        {
            _owner.StateUnit = StateUnit.Battle;
        }


        public void EnemyNoDetected()
        {
            _owner.StateUnit = StateUnit.Normal;
        }

        // //todo оптимизировать!!!
        // //todo ввести массив с расчетом дистанции, его отсортировать и после этого изменить порядок в массиве коллаидеров
        // //todo добавить проверку CheckBlocked на видимость каждого противника
        // private Collider[] SortingByRange(Collider[] array, int count)
        // {
        //     for (var i = 0; i < count - 1; i++)
        //     {
        //         for (var j = i + 1; j < count; j++)
        //         {
        //             if (Distance(_position, array[i].transform.position) >
        //                 Distance(_position, array[j].transform.position))
        //             {
        //                 var temp = array[i];
        //                 array[i] = array[j];
        //                 array[j] = temp;
        //             }
        //         }
        //     }
        //
        //     return array;
        // }
        //
        // private float Distance(Vector3 owner, Vector3 target)
        // {
        //     return (owner - target).magnitude;
        // }


        //
        //
        // public bool VisionM(Transform player, Transform target)
        // {
        //     return Distance(player, target) && Angle(player, target) && !CheckBloked(player, target);
        // }
        //
        // private bool CheckBloked(Transform player, Transform target)
        // {
        //     if (!Physics.Linecast(player.position, target.position, out var hit)) return true;
        //     return hit.transform != target;
        // }
        //
        // private bool Angle(Transform player, Transform target)
        // {
        //     var angle = Vector3.Angle(target.position - player.position, player.forward);
        //     return angle <= _activeAng;
        // }
        //
        // private bool Distance(Transform player, Transform target)
        // {
        //     return (player.position - target.position).sqrMagnitude <= _activeDis * _activeDis;
        // }

        #endregion

        public void DrawDebug()
        {
            Dbg.DebugWireSphere(_position, Color.yellow, _battleDistance);
            Dbg.DebugWireSphere(_position, Color.red, _equipmentSystem.AttackingWeapon.MaxAttackDistance);
        }
    }
}