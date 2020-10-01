using Controller;
using Controller.TimeRemaining;
using Helper;
using Interface;
using Manager;
using Model.Weapons;
using UnityEngine;


namespace Model.Player
{
    [System.Serializable]
    public sealed class BattleSystem
    {
        #region Fields

        private bool _isAutoAttack;

        private PlayerModel _owner;
        private VisionSystem _visionSystem;
        private MovementSystem _movementSystemSystem;
        private ITimeRemaining _timerToStrike;
        private ITimeRemaining _timerDisableBattleState;
        private bool _isEnabledTimer = false;

        [SerializeField] private float _timeDisableBattleState = 5.0f;
        [SerializeField] private Transform _barrel;

        private Vector3 _cashDirection;

        #endregion


        #region Properties

        public Weapon Weapon => _owner.EquipmentSystem.AttackingWeapon;

        public bool IsAutoAttack
        {
            get => _isAutoAttack;
            set => _isAutoAttack = value;
        }

        #endregion


        #region Constructor

        public void Constructor(PlayerModel playerModel, ref VisionSystem visionSystem,
            ref MovementSystem movementSystem)
        {
            _owner = playerModel;
            _visionSystem = visionSystem;
            _movementSystemSystem = movementSystem;

            _timerToStrike = new TimeRemaining(WaitAndAttack, Weapon._timeToStrike);
            _timerDisableBattleState = new TimeRemaining(DisableBattleState, _timeDisableBattleState);
        }

        #endregion


        public void Attack(BaseUnitModel target)
        {
            _timerDisableBattleState.RemoveTimeRemainingExecute();
            
            Dbg.Log($"BattleSystem.Attack");
            _movementSystemSystem.RotationToTarget(target);
            // var position = _owner.Transform.position;
            // position.y += 1.5f; // поднимаем на уровень глаз
            var heading = target.Transform.position - _barrel.transform.position;
            _cashDirection = heading / heading.magnitude;
            Dbg.Log($"BattleSystem.Attack.Weapon.IsReady = {Weapon.IsReady}");
            if (Weapon.IsReady)
            {
                Dbg.Log($"BattleSystem.Attack.Weapon._isEnabledTimer = {_isEnabledTimer}");
                if (!_isEnabledTimer)
                {
                    _timerToStrike.AddTimeRemainingExecute();
                    _isEnabledTimer = true;
                    _owner.AnimatorParams.AttackType = Weapon.RandomVariantAttack;
                    _owner.AnimatorParams.SetTriggerAttack();

                    // ServiceLocator.Resolve<AudioController>().PlaySwordAttack();
                    _owner.SoundPlayer.PlaySwordAttack();
                    Dbg.Log($"BattleSystem.Attack.SetTriggers");
                }
            }

            IsAutoAttack = true;
            _timerDisableBattleState.AddTimeRemainingExecute();
        }

        private void WaitAndAttack()
        {
            Dbg.Log($"BattleSystem.Attack.Weapon.WaitAndAttack Tick");
            Weapon.Attack(_barrel.position, _cashDirection, _visionSystem.EnemiesLayerMask);
            _owner.AnimatorParams.ResetTrigger(TagManager.ANIMATOR_PARAM_ATTACK_TRIGGER);
            _isEnabledTimer = false;
        }
        
        private void DisableBattleState()
        {
            IsAutoAttack = false;
            _visionSystem.EnemyNoDetected();
        }
    }
}