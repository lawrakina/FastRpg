using Controller;
using Controller.TimeRemaining;
using Helper;
using Interface;
using UnityEngine;


namespace Model.Weapons
{
    public abstract class BaseHit : BaseObjectScene
    {
        #region Fields

        [SerializeField] protected float _timeToDestruct = 0.2f;
        [SerializeField] protected float _baseDamage = 0.0f;
        protected ITimeRemaining _timePutToPool;
        protected float _weaponDamage = 0.0f;

        #endregion
        
        
        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
        }

        protected virtual void Start()
        {
            _timePutToPool = new TimeRemaining(DestroyAmmunition, _timeToDestruct);
            _timePutToPool.AddTimeRemainingExecute();
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            var tempObj = collision.gameObject.GetComponent<ICollision>();

            if (tempObj != null)
            {
                tempObj.OnCollision(new InfoCollision(_weaponDamage, collision.contacts[0],
                    collision.transform,
                    Rigidbody.velocity));
            }

            DestroyAmmunition();
        }

        #endregion


        #region Methods

        public virtual void AddForce(Vector3 dir, float weaponDamage)
        {
            if (!Rigidbody) return;
            _weaponDamage = _baseDamage + weaponDamage;
            Rigidbody.AddForce(dir);
        }

        protected abstract void DestroyAmmunition();

        #endregion
    }
}