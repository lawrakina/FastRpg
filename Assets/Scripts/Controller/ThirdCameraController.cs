using Interface;

namespace Controller
{
    public sealed class ThirdCameraController: IUpdated
    {
        #region Fields

        private CameraModel _cameraModel;
        private Transform _target;

        #endregion

        public ThirdCameraController()
        {
            _cameraModel = Object.FindObjectOfType<CameraModel>();
            _target = _context
            SearchPlayer();
        }
        
        #region UnityMethods
        
        private void SearchPlayer()
        {
            _target = Object.FindObjectOfType<PlayerModel>().Transform;
            _cameraModel.SetTargetTransform(_target);
        }

        #endregion

        public void UpdateTick()
        {
            _cameraModel.Execute();
        }
    }
}