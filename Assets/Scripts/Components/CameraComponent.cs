using Interface;
using UnityEngine;


namespace Components
{
    public sealed class CameraComponent : MonoBehaviour, IUpdated
    {
        #region Fields

        private Transform _target;
        [SerializeField] private Vector3 _offset = new Vector3(0.0f, 25.0f, -12.0f);
        [SerializeField] private bool _enableOffsetAxisX = false;

        #endregion


        #region Methods

        public void SetTargetTransform(Transform playerTransform)
        {
            _target = playerTransform;
        }

        #endregion

        public void UpdateTick()
        {
            if (_target == null) return;

            if (_enableOffsetAxisX)
                transform.position = _target.position + _offset;
            else
            {
                transform.position = new Vector3(
                    transform.position.x,
                    _target.position.y + _offset.y,
                    _target.position.z + _offset.z
                );
            }
        }
    }
}