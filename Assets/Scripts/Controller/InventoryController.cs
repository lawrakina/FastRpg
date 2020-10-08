using System.Collections.Generic;
using System.Linq;
using Enums;
using Interface;
using Model;
using Model.Player;
using Model.Weapons;
using UnityEngine;


namespace Controller
{
    public sealed class InventoryController : BaseController, IInitialization
    {
        #region Fields

        private InventoryModel _inventoryModel;
        private List<Weapon> _weapons = new List<Weapon>();
        private int _selectIndexWeapon;

        #endregion


        #region Properties

        public List<Weapon> Weapons => _weapons;

        #endregion


        #region IInitialization

        public void Initialization()
        {
            // _weapons = ServiceLocatorMonoBehaviour.GetService<CharacterController>().GetComponentsInChildren<Weapon>()
            // .ToList();
            // _weapons = ServiceLocatorMonoBehaviour.GetService<InventoryModel>().GetComponentInChildren<Weapon>()

            _inventoryModel = Object.FindObjectOfType<InventoryModel>();
            _weapons = _inventoryModel.GetComponentsInChildren<Weapon>().ToList();
            Debug.Log($"InventoryController.Initialization._weapons:{_weapons.Count}:{_weapons}");
            // foreach (var weapon in Weapons)
            // {
            //     weapon.IsVisible = false;
            // }
        }

        #endregion


        #region Methods
        
        

        public Weapon SelectWeapon(int weaponNumber)
        {
            if (weaponNumber < 0 || weaponNumber >= Weapons.Count) return null;

            var tempWeapon = Weapons[weaponNumber];
            return tempWeapon;
        }

        public Weapon SelectWeapon(MouseScrollWheel scrollWheel)
        {
            if (scrollWheel == MouseScrollWheel.Up)
            {
                if (_selectIndexWeapon < Weapons.Count - 1)
                {
                    _selectIndexWeapon++;
                }
                else
                {
                    _selectIndexWeapon = -1;
                }

                return SelectWeapon(_selectIndexWeapon);
            }

            if (_selectIndexWeapon <= 0)
            {
                _selectIndexWeapon = Weapons.Count;
            }
            else
            {
                _selectIndexWeapon--;
            }

            return SelectWeapon(_selectIndexWeapon);
        }

        // public void AddWeapon(Weapon fpsWeapon)
        // {
        // }
        //
        // public void RemoveWeapon()
        // {
        //     var selectWeapon = SelectWeapon(_selectIndexWeapon);
        //     if (selectWeapon)
        //     {
        //         Weapons.Remove(selectWeapon);
        //         selectWeapon.transform.parent = null;
        //         selectWeapon..SetActive(true);
        //     }
        // }

        #endregion

        public Weapon SelectWeaponByType(int id)
        {
            foreach (var item in _weapons)
            {
                if ((int) item.WeaponType == id)
                {
                    Debug.Log($"InventoryController.SelectWeaponByType.id:{id}, item.WeaponType:{item.WeaponType}, item:{item}");
                    return item;
                }
                    
            }

            return null;
        }

        public bool PickUpItem(Weapon weapon)
        {
            weapon.transform.SetParent(_inventoryModel.Transform);
            weapon.transform.position = _inventoryModel.Transform.position; 
            return true;
        }

        public void AddItem(GameObject item)
        {
            var weapon = item.GetComponent<Weapon>();
            _weapons.Add(weapon);
            
        }
    }
}