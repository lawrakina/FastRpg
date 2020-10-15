using Interface;
using UnityEngine;
using VIew;


namespace Controller
{
    public sealed class ThirdCameraController: IUpdated
    {
        #region Fields
    
        private CameraView _cameraView;
        private Transform _target;
    
        #endregion
    
        public ThirdCameraController(CameraView camera, GameContext context)
        {
            _cameraView = camera;
            _target = context.PlayerData.PlayerStruct.Player.transform;
        }
        
        // public void SearchPlayer()
        // {
        //     _target = Object.FindObjectOfType<PlayerComponent>().Transform;
        //     if(_target)
        //         _cameraComponent.SetTargetTransform(_target);
        // }
        
        #region IUpdated

        public void UpdateTick()
        {
            if (_target == null) return;

            if (_cameraView._enableOffsetAxisX)
                _cameraView.transform.position = _target.position + _cameraView._offset;
            else
            {
                _cameraView.transform.position = new Vector3(
                    _cameraView.transform.position.x,
                    _target.position.y + _cameraView._offset.y,
                    _target.position.z + _cameraView._offset.z
                );
            }

            var lenght = Vector3.Distance(_cameraView.transform.position, _target.transform.position);
            _cameraView.Collider.center = _cameraView._offsetCollider;
            _cameraView.Collider.height = lenght * 2 - _cameraView._offsetHeight;
            _cameraView.Collider.radius = _cameraView._offsetRadius;
        }

        #endregion
    }
}