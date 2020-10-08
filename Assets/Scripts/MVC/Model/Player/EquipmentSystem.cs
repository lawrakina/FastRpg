using System;
using Controller;
using Enums;
using Helper;
using Manager;
using Model.Weapons;
using UnityEngine;


namespace Model.Player
{
    [System.Serializable]
    public sealed class EquipmentSystem
    {
        // private class AttachPoint
        // {
        //     public int Id;
        //     
        // }

        #region Fields

        // [SerializeField] private string _nameAttachPointWeaponRightHand = "Weapon_Attachment_Point_Right";
        // [SerializeField] private string _nameAttachPointWeaponLeftHand = "Weapon_Attachment_Point_Left";
        // [SerializeField] private string _nameAttachPointShieldLeftHand = "Shield_Attachment_Point_Left";

        private PlayerModel _owner;
        private BattleSystem _battleSystem;

        [SerializeField] private Transform _attachPointWeaponRightHand;
        [SerializeField] private Transform _attachPointWeaponLeftHand;
        [SerializeField] private Transform _attachPointShieldLeftHand;
        [SerializeField] private Transform _attachPointHiddenMainWeapon;
        [SerializeField] private Transform _attachPointHiddenSecondWeapon;
        [SerializeField] private Transform _attachPointHiddenShield;
        [SerializeField] private Transform _attachPointHidden2HandWeapon;

        [SerializeField] private Weapon _mainWeapon;
        [SerializeField] private Weapon _secondWeapon;

        // private GameObject _gameObjectMainWeapon;
        // private GameObject _gameObjectSecondWeapon;

        [SerializeField] private Weapon _unarmedRight;
        [SerializeField] private Weapon _unarmedLeft;

        public Weapon AttackingWeapon
        {
            get
            {
                if (_mainWeapon.MaxAttackDistance > _secondWeapon.MaxAttackDistance)
                    return _mainWeapon;
                else
                    return _secondWeapon;
            }
        }

        #endregion


        #region Constructor

        public void Constructor(PlayerModel playerModel,ref BattleSystem battleSystem)
        {
            _owner = playerModel;
            _battleSystem = battleSystem;

            _mainWeapon = _unarmedRight;
            _secondWeapon = _unarmedLeft;

            // _attachPointWeaponRightHand = _owner.Transform.Find(_nameAttachPointWeaponRightHand);
            // _attachPointWeaponLeftHand = _owner.Transform.Find(_nameAttachPointWeaponLeftHand);
            // _attachPointShieldLeftHand = _owner.Transform.Find(_nameAttachPointShieldLeftHand);
        }

        #endregion


        #region Methods

        public void UnsheathWeapons()
        {
            switch (_mainWeapon.WeaponType)
            {
                case WeaponType.Sword:
                    _mainWeapon.transform.SetParent(_attachPointWeaponRightHand,false);
                    break;
                case WeaponType.Mace:
                    _mainWeapon.transform.SetParent(_attachPointWeaponRightHand,false);
                    break;
                case WeaponType.Dagger:
                    _mainWeapon.transform.SetParent(_attachPointWeaponRightHand,false);
                    break;
                case WeaponType.Axe:
                    _mainWeapon.transform.SetParent(_attachPointWeaponRightHand,false);
                    break;
                case WeaponType.TwoHandAxe:
                    _mainWeapon.transform.SetParent(_attachPointWeaponRightHand,false);
                    break;
                case WeaponType.TwoHandSpear:
                    _mainWeapon.transform.SetParent(_attachPointWeaponRightHand,false);
                    break;
                case WeaponType.TwoHandSword:
                    _mainWeapon.transform.SetParent(_attachPointWeaponRightHand,false);
                    break;
                case WeaponType.TwoHandStaff:
                    _mainWeapon.transform.SetParent(_attachPointWeaponRightHand,false);
                    break;
                case WeaponType.TwoHandCrossbow:
                    _mainWeapon.transform.SetParent(_attachPointHidden2HandWeapon,false);
                    break;
            }


            switch (_secondWeapon.WeaponType)
            {
                case WeaponType.TwoHandBow:
                    _secondWeapon.transform.SetParent(_attachPointWeaponLeftHand,false);
                    break;
                case WeaponType.Shield:
                    _secondWeapon.transform.SetParent(_attachPointShieldLeftHand,false);
                    break;
            }
        }

        public void SheathWeapon()
        {
            switch (_mainWeapon.WeaponType)
            {
                case WeaponType.Sword:
                    _mainWeapon.transform.SetParent(_attachPointHiddenMainWeapon,false);
                    break;
                case WeaponType.Mace:
                    _mainWeapon.transform.SetParent(_attachPointHiddenMainWeapon,false);
                    break;
                case WeaponType.Dagger:
                    _mainWeapon.transform.SetParent(_attachPointHiddenMainWeapon,false);
                    break;
                case WeaponType.Axe:
                    _mainWeapon.transform.SetParent(_attachPointHiddenMainWeapon,false);
                    break;
                case WeaponType.Shield:
                    _mainWeapon.transform.SetParent(_attachPointShieldLeftHand,false);
                    break;
                case WeaponType.TwoHandAxe:
                    _mainWeapon.transform.SetParent(_attachPointHidden2HandWeapon,false);
                    break;
                case WeaponType.TwoHandSpear:
                    _mainWeapon.transform.SetParent(_attachPointHidden2HandWeapon,false);
                    break;
                case WeaponType.TwoHandSword:
                    _mainWeapon.transform.SetParent(_attachPointHidden2HandWeapon,false);
                    break;
                case WeaponType.TwoHandStaff:
                    _mainWeapon.transform.SetParent(_attachPointHidden2HandWeapon,false);
                    break;
                case WeaponType.TwoHandBow:
                    _mainWeapon.transform.SetParent(_attachPointHidden2HandWeapon,false);
                    break;
                case WeaponType.TwoHandCrossbow:
                    _mainWeapon.transform.SetParent(_attachPointHidden2HandWeapon,false);
                    break;
            }

            switch (_secondWeapon.WeaponType)
            {
                case WeaponType.Sword:
                    _secondWeapon.transform.SetParent(_attachPointHiddenSecondWeapon,false);
                    break;
                case WeaponType.Mace:
                    _secondWeapon.transform.SetParent(_attachPointHiddenSecondWeapon,false);
                    break;
                case WeaponType.Dagger:
                    _secondWeapon.transform.SetParent(_attachPointHiddenSecondWeapon,false);
                    break;
                case WeaponType.Axe:
                    _secondWeapon.transform.SetParent(_attachPointHiddenSecondWeapon,false);
                    break;
                case WeaponType.Shield:
                    _secondWeapon.transform.SetParent(_attachPointHiddenShield,false);
                    break;
                case WeaponType.TwoHandAxe:
                    _secondWeapon.transform.SetParent(_attachPointHidden2HandWeapon,false);
                    break;
                case WeaponType.TwoHandSpear:
                    _secondWeapon.transform.SetParent(_attachPointHidden2HandWeapon,false);
                    break;
                case WeaponType.TwoHandSword:
                    _secondWeapon.transform.SetParent(_attachPointHidden2HandWeapon,false);
                    break;
                case WeaponType.TwoHandStaff:
                    _secondWeapon.transform.SetParent(_attachPointHidden2HandWeapon,false);
                    break;
                case WeaponType.TwoHandBow:
                    _secondWeapon.transform.SetParent(_attachPointHidden2HandWeapon,false);
                    break;
                case WeaponType.TwoHandCrossbow:
                    _secondWeapon.transform.SetParent(_attachPointHidden2HandWeapon,false);
                    break;
            }
        }

        public void Equip(Weapon weapon)
        {
            Debug.Log($"EquipmentSystem.Equip.Weapon:{weapon}");
            switch (weapon.WeaponType)
            {
                case WeaponType.Unarmed:
                    RemoveAllWeapons();
                    break;
                case WeaponType.Sword:
                    EquipMainWeapon(weapon);
                    break;
                case WeaponType.Mace:
                    EquipMainWeapon(weapon);
                    break;
                case WeaponType.Dagger:
                    EquipMainWeapon(weapon);
                    break;
                case WeaponType.Axe:
                    EquipMainWeapon(weapon);
                    break;
                case WeaponType.Shield:
                    RemoveAllWeapons();
                    EquipShield(weapon);
                    break;
                case WeaponType.OneHandSwordShield:
                    EquipMainWeapon(weapon);
                    EquipShield(weapon);
                    break;
                case WeaponType.OneHandMaceShield:
                    EquipMainWeapon(weapon);
                    EquipShield(weapon);
                    break;
                case WeaponType.OneHandDaggerShield:
                    EquipMainWeapon(weapon);
                    EquipShield(weapon);
                    break;
                case WeaponType.OneHandAxeShield:
                    EquipMainWeapon(weapon);
                    EquipShield(weapon);
                    break;
                case WeaponType.TwoHandAxe:
                    RemoveAllWeapons();
                    EquipMainWeapon(weapon);
                    break;
                case WeaponType.TwoHandSpear:
                    RemoveAllWeapons();
                    EquipMainWeapon(weapon);
                    break;
                case WeaponType.TwoHandSword:
                    RemoveAllWeapons();
                    EquipMainWeapon(weapon);
                    break;
                case WeaponType.TwoHandStaff:
                    RemoveAllWeapons();
                    EquipMainWeapon(weapon);
                    break;
                case WeaponType.TwoHandBow:
                    RemoveAllWeapons();
                    EquipSecondWeapon(weapon);
                    break;
                case WeaponType.TwoHandCrossbow:
                    RemoveAllWeapons();
                    EquipMainWeapon(weapon);
                    break;
                default:
                    RemoveAllWeapons();
                    break;
            }
        }
        
        #endregion


        #region PrivateMethods

        private void EquipMainWeapon(Weapon weapon)
        {
            EquipWeapon(ref _mainWeapon, _attachPointWeaponRightHand, weapon);
        }

        private void EquipSecondWeapon(Weapon weapon)
        {
            EquipWeapon(ref _secondWeapon, _attachPointWeaponLeftHand, weapon);
        }

        private void EquipShield(Weapon weapon)
        {
            EquipWeapon(ref _secondWeapon, _attachPointShieldLeftHand, weapon);
        }

        private void RemoveAllWeapons()
        {
            RemoveItemsFrom(_attachPointWeaponRightHand);
            RemoveItemsFrom(_attachPointWeaponLeftHand);
            RemoveItemsFrom(_attachPointShieldLeftHand);
            RemoveItemsFrom(_attachPointHiddenShield);
            RemoveItemsFrom(_attachPointHidden2HandWeapon);
            RemoveItemsFrom(_attachPointHiddenMainWeapon);
            RemoveItemsFrom(_attachPointHiddenSecondWeapon);

            _mainWeapon = _unarmedRight;
            _secondWeapon = _unarmedLeft;
            CalculateWeaponTypeForAnimator();
        }

        private void EquipWeapon(ref Weapon slotWeapon, Transform attachPoint, Weapon weapon)
        {
            slotWeapon = weapon;
            slotWeapon.transform.SetParent(attachPoint, false);
            weapon.transform.position = slotWeapon.transform.position;
            weapon.transform.localRotation = Quaternion.identity;
            // weapon.transform.localScale = Vector3.one;
            CalculateWeaponTypeForAnimator();
        }

        private void RemoveItemsFrom(Component attachPoint)
        {
            foreach (var child in attachPoint.GetComponentsInChildren<Weapon>())
            {
                ServiceLocator.Resolve<InventoryController>().PickUpItem(child);
            }
        }

        private void CalculateWeaponTypeForAnimator()
        {
            var num = 0;
            if (_mainWeapon != null)
                num = (int) _mainWeapon.WeaponType;
            if (_secondWeapon != null)
                num += (int) _secondWeapon.WeaponType;
            Dbg.Log($"_mainWeapon.WeaponType:{_mainWeapon.WeaponType}, _secondWeapon.WeaponType:{_secondWeapon.WeaponType}, num:{num}");
            _owner.AnimatorParams.WeaponType = num;
        }

        #endregion
    }
}