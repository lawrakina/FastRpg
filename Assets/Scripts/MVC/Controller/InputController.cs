using Enums;
using Interface;
using Model;
using UnityEngine;
using Cursor = UnityEngine.Cursor;


namespace MVC.Controller
{
    public sealed class InputController : BaseController, IExecute
    {
        #region Fields

        // private KeyCode _activeFlashLight = KeyCode.F;
        // private KeyCode _cancel = KeyCode.Escape;
        // private KeyCode _reloadClip = KeyCode.R;
        // private KeyCode _removeWeapon = KeyCode.T;
        private KeyCode _selectWeapon1 = KeyCode.Alpha1;
        private KeyCode _selectWeapon2 = KeyCode.Alpha2;
        private KeyCode _selectWeapon3 = KeyCode.Alpha3;
        private KeyCode _selectWeapon4 = KeyCode.Alpha4;
        // private int _mouseButton = (int)MouseButton.LeftButton;
        private Vector3 _inputVector;

        #endregion


        #region IExecute

        public void Execute()
        {
            if (!IsActive) return;
            // if (Input.GetKeyDown(_activeFlashLight))
            // {
            //     ServiceLocator.Resolve<FlashLightController>().Switch();
            // }
            // if (Input.GetKeyDown(_selectWeapon1))
            // {
            //     SelectWeapon(0);
            // }
            //
            // if (Input.GetKeyDown(_selectWeapon2))
            // {
            //     SelectWeapon(1);
            // }
            // else if (Input.GetKeyDown(_cancel))
            // {
            //     ServiceLocator.Resolve<WeaponController>().Off();
            //     ServiceLocator.Resolve<FlashLightController>().Off();
            // } 
            // else if (Input.GetKeyDown(_reloadClip))
            // {
            //     ServiceLocator.Resolve<WeaponController>().ReloadClip();
            // } 
            // else if (Input.GetKeyDown(_removeWeapon))
            // {
            //     ServiceLocator.Resolve<WeaponController>().Off();
            //     ServiceLocator.Resolve<Inventory>().RemoveWeapon();
            // }

            // if (Input.GetMouseButton(_mouseButton))
            // {
            //     if (ServiceLocator.Resolve<WeaponController>().IsActive)
            //     {
            //         ServiceLocator.Resolve<WeaponController>().Fire();
            //     }
            // }

            // if (Input.GetAxis("Mouse ScrollWheel") > 0) // todo manager
            // {
            //     MouseScroll(MouseScrollWheel.Up);
            // }
            //
            // if (Input.GetAxis("Mouse ScrollWheel") < 0)
            // {
            //     MouseScroll(MouseScrollWheel.Down);
            // }

            _inputVector = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            // _inputVector = new Vector3(
            //     UltimateJoystick.GetHorizontalAxis("Movement"),
            //     0.0f,
            //     UltimateJoystick.GetVerticalAxis("Movement"));
            ServiceLocator.Resolve<PlayerController>().Move(_inputVector);

            if (Input.GetKeyDown(_selectWeapon1))
            {
                ServiceLocator.Resolve<InputController>().EquipWeapon(0);
            }
            if (Input.GetKeyDown(_selectWeapon2))
            {
                ServiceLocator.Resolve<InputController>().EquipWeapon(1);
            }
            if (Input.GetKeyDown(_selectWeapon3))
            {
                ServiceLocator.Resolve<InputController>().EquipWeapon(21);
            }
            if (Input.GetKeyDown(_selectWeapon4))
            {
                ServiceLocator.Resolve<InputController>().EquipWeapon(23);
            }
        }

        #endregion


        #region Methods

        public void EquipWeapon(int id)
        {
            var tempWeapon = ServiceLocator.Resolve<InventoryController>().SelectWeaponByType(id);
            Debug.Log($"ServiceLocator.Resolve<InventoryController>().SelectWeaponByType({id});");
            Debug.Log($"tempWeapon:{tempWeapon},{tempWeapon.WeaponType};");

            ServiceLocator.Resolve<PlayerController>().EquipWeapon(tempWeapon);

            // ServiceLocator.Resolve<WeaponController>().Off();
            // if (tempWeapon != null)
            // {
            //     ServiceLocator.Resolve<WeaponController>().On(tempWeapon);
            // }
        }


        // private void MouseScroll(MouseScrollWheel value)
        // {
        //     var tempWeapon = ServiceLocator.Resolve<Inventory>().SelectWeapon(value);
        //     SelectWeapon(tempWeapon);
        // }

        #endregion
    }
}