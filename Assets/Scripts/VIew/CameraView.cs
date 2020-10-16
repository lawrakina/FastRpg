using System;
using UnityEngine;


namespace VIew
{
    public sealed class CameraView : MonoBehaviour
    {
        #region Fields

        // private Transform _target;
        public Vector3 _offset = new Vector3(0.0f, 18.0f, -8.0f);
        public Vector3 _offsetCollider = new Vector3(0.0f, 2.6f, 0.0f);
        public float _offsetHeight = 9.1f;
        public float _offsetRadius = 5.2f;
        public bool _enableOffsetAxisX = true;
        [HideInInspector] public CapsuleCollider Collider;

        #endregion


        #region Events

        public event Action<Collider> OnCollisionEnter;
        public event Action<Collider> OnCollisionExit;

        #endregion

        private void Awake()
        {
            Collider = GetComponent<CapsuleCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            OnCollisionEnter?.Invoke(other);
        }
        
        private void OnTriggerExit(Collider other)
        {
            OnCollisionExit?.Invoke(other);
        }
    }
}