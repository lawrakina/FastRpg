using Controller;
using Controller.TimeRemaining;
using Enums;
using UnityEngine;


namespace Model.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        #region Fields

        [SerializeField] protected float _rechergeTime = 1.0f;
        [SerializeField] protected float _damage = 10.0f;
        [SerializeField] public float _timeToStrike = 0.2f;
        [SerializeField] private float _attackDistance = 2.0f;
        [SerializeField] protected BaseHit _standardHit;

        protected bool _isReady = true;
        protected ITimeRemaining _timeRemaining;

        #endregion


        #region Properties

        public float MaxAttackDistance => _attackDistance;
        public float RecommendedAttackDistance => _attackDistance * 0.9f;
        public abstract WeaponAttachmentPoint AttachmentPoint { get; }
        public abstract WeaponType WeaponType { get; }
        protected abstract int CountVariantsAttack { get; }
        [HideInInspector] public int RandomVariantAttack => Random.Range(0, CountVariantsAttack - 1);
        public bool IsReady => _isReady;

        #endregion


        #region UnityMethods

        protected virtual void Start()
        {
            _timeRemaining = new TimeRemaining(ReadyAttack, _rechergeTime);
        }

        #endregion


        #region Methods

        protected void ReadyAttack()
        {
            _isReady = true;
        }
        
        public abstract void Attack(Vector3 transformPosition, Vector3 direction, LayerMask enemiesLayerMask);

        protected void StandardAttack(Vector3 transformPosition, Vector3 direction, LayerMask enemiesLayerMask)
        {
            if (!_isReady) return;
            if (direction != Vector3.zero)
            {
                RaycastHit hit;
                if (Physics.Raycast(transformPosition, direction, out hit, MaxAttackDistance, enemiesLayerMask))
                {
                    // Dbg.DrawRay(transformPosition, direction * hit.distance, Color.yellow);
                    // Dbg.Log("Did Hit");

                    var tempAmmunition = ServiceLocator.Resolve<PoolController>().GetFromPool(_standardHit) as StandardHit;
                    tempAmmunition.transform.position = transformPosition;
                    tempAmmunition.transform.rotation = Quaternion.LookRotation(direction);
                    tempAmmunition.AddForce(direction * 1000f, _damage);
                    _isReady = false;
                    _timeRemaining.AddTimeRemainingExecute();
                }
                else
                {
                    // Dbg.DrawRay(transformPosition, direction * 1000, Color.white);
                    // Dbg.Log("Did not Hit");
                }
            }
        }


        #endregion

    }
}