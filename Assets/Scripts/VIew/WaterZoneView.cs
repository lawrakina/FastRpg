using System;
using UnityEngine;


namespace VIew
{
    public class WaterZoneView : MonoBehaviour
    {
        #region Events

        public event Action<Collider> OnEnter;
        public event Action<Collider> OnExit;

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"OnTriggerEnter(Collider other)");
            OnEnter?.Invoke(other);
        }
        
        private void OnTriggerExit(Collider other)
        {
            Debug.Log($"OnTriggerExit(Collider other)");
            OnExit?.Invoke(other);
        }
    }
}
