using UnityEngine;

namespace Controller
{
    public sealed class InputController
    {
        #region Fields

        private Vector2 _inputVector;
        private Services _services;

        public InputController(Services services)
        {
            _services = services;
        }

        #endregion

        #region IUdpatable

        public void UpdateTick()
        {
            // _inputVector = new Vector2(
            //     UltimateJoystick.GetHorizontalAxis("Movement"),
            //     UltimateJoystick.GetVerticalAxis("Movement"));
            _inputVector = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            _services.PlayerController.Move(_inputVector);
        }

        #endregion
    }
}