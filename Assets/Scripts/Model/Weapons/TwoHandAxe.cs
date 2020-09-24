using Enums;
using UnityEngine;

namespace Model.Weapons
{
    [System.Serializable]
    public sealed class TwoHandAxe: Weapon
    {
        #region Fields

        [SerializeField] private WeaponAttachmentPoint _attachmentPoint = WeaponAttachmentPoint.TwoHandsRight;
        [SerializeField] private WeaponType _weaponType = WeaponType.TwoHandAxe;

        #endregion

        
        #region Properties

        public override WeaponAttachmentPoint AttachmentPoint => _attachmentPoint;
        public override WeaponType WeaponType => _weaponType;
        protected override int CountVariantsAttack  => 6;

        #endregion

        public override void Attack(Vector3 transformPosition, Vector3 direction, LayerMask enemiesLayerMask)
        {
            StandardAttack(transformPosition,direction, enemiesLayerMask);
        }
    }
}