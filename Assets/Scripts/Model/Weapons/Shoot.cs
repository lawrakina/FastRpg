using Controller;
using Controller.TimeRemaining;
using Enums;
using Helper;
using Interface;
using UnityEngine;

namespace Model.Weapons
{
    public sealed class Shoot: BaseHit
    {
        #region Fields

        protected float _curDamage;
        private float _lossOfDamageAtTime = 0.2f;

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            _curDamage = _baseDamage;
        }

        protected override void Start()
        {
            _timePutToPool = new TimeRemaining(DestroyAmmunition, _timeToDestruct);
            _timePutToPool.AddTimeRemainingExecute();
            
            InvokeRepeating(nameof(LossOfDamage), 0, 1);
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);
        }
        
        #endregion


        #region Methods
        
        private void LossOfDamage()
        {
            _curDamage -= _lossOfDamageAtTime;
        }
        
        protected override void DestroyAmmunition()
        {
            CancelInvoke(nameof(LossOfDamage));

            ToDefault();
            _timePutToPool.RemoveTimeRemainingExecute();
            ServiceLocator.Resolve<PoolController>().PutToPool(this);
        }
        
        private void ToDefault()
        {
            _curDamage = _baseDamage;
        }
        #endregion
    }
}