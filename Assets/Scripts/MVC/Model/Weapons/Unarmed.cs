using Controller;
using Controller.TimeRemaining;
using Enums;
using Helper;
using UnityEngine;


namespace Model.Weapons
{
    [System.Serializable]
    public sealed class Unarmed : Weapon
    {
        #region Fields

        [SerializeField] private WeaponAttachmentPoint _attachmentPoint = WeaponAttachmentPoint.Unarmed;
        [SerializeField] private WeaponType _weaponType = WeaponType.Unarmed;

        #endregion


        #region Properties

        public override WeaponAttachmentPoint AttachmentPoint => _attachmentPoint;
        public override WeaponType WeaponType => _weaponType;
        protected override int CountVariantsAttack => 6;

        #endregion

        protected override void Start()
        {
            base.Start();
        }

        public override void Attack(Vector3 transformPosition, Vector3 direction, LayerMask enemiesLayerMask)
        {
            StandardAttack(transformPosition,direction, enemiesLayerMask);
        }


        // public override void Attack(Vector3 transformPosition, Vector3 direction, LayerMask enemiesLayerMask)
        // {
        //     if (!_isReady) return;
        //     if (direction != Vector3.zero)
        //     {
        //         RaycastHit hit;
        //         if (Physics.Raycast(transformPosition, direction, out hit, MaxAttackDistance, enemiesLayerMask))
        //         {
        //             // Dbg.DrawRay(transformPosition, direction * hit.distance, Color.yellow);
        //             // Dbg.Log("Did Hit");
        //
        //             var tempAmmunition = ServiceLocator.Resolve<PoolController>().GetFromPool(_hitType) as Hit;
        //             tempAmmunition.transform.position = transformPosition;
        //             tempAmmunition.transform.rotation = Quaternion.LookRotation(direction);
        //             tempAmmunition.AddForce(direction * 1000f );
        //             _isReady = false;
        //             _timeRemaining.AddTimeRemainingExecute();
        //         }
        //         else
        //         {
        //             // Dbg.DrawRay(transformPosition, direction * 1000, Color.white);
        //             // Dbg.Log("Did not Hit");
        //         }
        //     }
    }
}