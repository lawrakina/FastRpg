using System;
using UnityEngine;

namespace Model
{
    public class HealthBar : MonoBehaviour {

        #region Fields

        private MaterialPropertyBlock _matBlock;
        private MeshRenderer _meshRenderer;
        private Camera _mainCamera;
        private BaseUnitModel _baseUnitModel;

        #endregion

        #region UnityMethods

        private void Awake() {
            _meshRenderer = GetComponent<MeshRenderer>();
            _matBlock = new MaterialPropertyBlock();
            // get the damageable parent we're attached to
            _baseUnitModel = GetComponentInParent<BaseUnitModel>();
        }

        private void Start() {
            // Cache since Camera.main is super slow
            _mainCamera = Camera.main;
        }

        private void Update() {
            // Only display on partial health
            if (_baseUnitModel.Hp <= _baseUnitModel.MaxHp) {
                _meshRenderer.enabled = true;
                // AlignCamera();
                UpdateParams();
            } else {
                _meshRenderer.enabled = false;
            }
        }

        private void LateUpdate()
        {
            if (_baseUnitModel.Hp <= _baseUnitModel.MaxHp)
                AlignCamera();
        }

        #endregion

        private void UpdateParams() {
            _meshRenderer.GetPropertyBlock(_matBlock);
            _matBlock.SetFloat("_Fill", _baseUnitModel.Hp / (float)_baseUnitModel.MaxHp);
            _meshRenderer.SetPropertyBlock(_matBlock);
        }

        private void AlignCamera() {
            if (_mainCamera != null) {
                var camXform = _mainCamera.transform;
                var forward = transform.position - camXform.position;
                forward.Normalize();
                var up = Vector3.Cross(forward, camXform.right);
                transform.rotation = Quaternion.LookRotation(forward, up);
            }
        }

    }
}