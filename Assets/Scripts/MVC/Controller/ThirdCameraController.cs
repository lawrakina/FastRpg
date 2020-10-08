using Interface;
using Model;
using Model.Player;
using UnityEngine;

namespace Controller
{
    public sealed class ThirdCameraController: BaseController, IInitialization, IExecute
    {
        #region Fields

        private CameraModel _cameraModel;
        private Transform _playerTransform;

        #endregion


        #region UnityMethods

        public void Initialization()
        {
            base.On();
            _cameraModel = Object.FindObjectOfType<CameraModel>();
            SearchPlayer();
        }

        public AudioSource GetAudioSourceCamera()
        {
            var result = _cameraModel.GetComponentInChildren<AudioSource>();
            return result;
        }

        private void SearchPlayer()
        {
            _playerTransform = Object.FindObjectOfType<PlayerModel>().Transform;
            _cameraModel.SetTargetTransform(_playerTransform);
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (!IsActive) return;
            
            _cameraModel.Execute();
        }

        #endregion
    }
}