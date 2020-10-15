using System;
using UnityEngine;
using UnityEngine.Rendering;


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

        private void Awake()
        {
            Collider = GetComponent<CapsuleCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var meshRenderer = other.GetComponent<MeshRenderer>();
            if(meshRenderer == null) return;
            if (meshRenderer.shadowCastingMode != ShadowCastingMode.ShadowsOnly)
                meshRenderer.shadowCastingMode = ShadowCastingMode.ShadowsOnly;
        }
        
        private void OnTriggerExit(Collider other)
        {
            var meshRenderer = other.GetComponent<MeshRenderer>();
            if(meshRenderer == null) return;
            if (meshRenderer.shadowCastingMode == ShadowCastingMode.ShadowsOnly)
                meshRenderer.shadowCastingMode = ShadowCastingMode.TwoSided;
        }
    }
}