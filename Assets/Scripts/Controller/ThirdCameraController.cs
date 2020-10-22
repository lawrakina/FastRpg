using Interface;
using Manager;
using UnityEngine;
using UnityEngine.Rendering;
using VIew;


namespace Controller
{
    public sealed class ThirdCameraController: IUpdated, IEnabled
    {
        #region Fields
    
        private CameraView _cameraView;
        private GameContext _context;
        private Transform _target;
    
        #endregion


        #region Property

        public Transform CameraTransform => _cameraView.transform;

        #endregion
        
        
        #region ctor

        public ThirdCameraController(CameraView camera, GameContext context)
        {
            _cameraView = camera;
            _context = context;
            _target = context.PlayerData.PlayerStruct.Player.transform;
        }


        #endregion


        #region UnityMethods

        public void On()
        {
            if (_cameraView == null) return;
            _cameraView.OnCollisionEnter += HideObject;
            _cameraView.OnCollisionExit += ShowObject;
        }

        public void Off()
        {
            if (_cameraView == null) return;
            _cameraView.OnCollisionEnter -= HideObject;
            _cameraView.OnCollisionExit -= ShowObject;
        }

        #endregion
        
        
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

        
        #region PrivateMethods

        private void HideObject(Collider obj)
        {
            if(!Helper.CheckForComparerLayer(_context.ObjectsToHideLayer, obj)) return;

            var meshRenderer = obj.GetComponent<MeshRenderer>();
            if(meshRenderer == null) return;
            
            meshRenderer.shadowCastingMode = ShadowCastingMode.ShadowsOnly;
        }
        
        private void ShowObject(Collider obj)
        {
            if(!Helper.CheckForComparerLayer(_context.ObjectsToHideLayer, obj)) return;

            var meshRenderer = obj.GetComponent<MeshRenderer>();
            if(meshRenderer == null) return;
            
            meshRenderer.shadowCastingMode = ShadowCastingMode.TwoSided;
        }

        #endregion
    }
}