using System;
using Extension;
using UnityEngine;

namespace InputMovement
{
    public sealed class MobileInputHorizontal : IUserInputProxy
    {
        public event Action<float> AxisOnChange = delegate(float f) {  };
        
        public void GetAxis()
        {
            // AxisOnChange.Invoke(Input.GetAxis(StringManager.AXIS_HORIZONTAL));
            AxisOnChange.Invoke(UltimateJoystick.GetHorizontalAxis(StringManager.ULTIMATE_JOYSTICK_MOVENMENT));
            // Debug.Log($"MobileInputHoriz:{UltimateJoystick.GetHorizontalAxis(StringManager.ULTIMATE_JOYSTICK_MOVENMENT)}");
        }
    }
}