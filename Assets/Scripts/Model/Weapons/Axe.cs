using Enums;
using UnityEngine;

namespace Model.Weapons
{
    [System.Serializable]
    public sealed class Axe: Weapon
    {
        #region Fields

        [SerializeField] private WeaponAttachmentPoint _attachmentPoint = WeaponAttachmentPoint.OnlyRight;
        [SerializeField] private WeaponType _weaponType = WeaponType.Axe;

        #endregion

        
        #region Properties

        public override WeaponAttachmentPoint AttachmentPoint => _attachmentPoint;
        public override WeaponType WeaponType => _weaponType;
        protected override int CountVariantsAttack => 3;

        #endregion

        public override void Attack(Vector3 transformPosition, Vector3 direction, LayerMask enemiesLayerMask)
        {
            StandardAttack(transformPosition,direction, enemiesLayerMask);
        }
    }
}