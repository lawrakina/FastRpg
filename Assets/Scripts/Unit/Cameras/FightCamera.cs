using Interface;
using UnityEngine;


namespace Unit.Cameras
{
    public sealed class FightCamera : MonoBehaviour, IFightCamera
    {
        #region Fields
        
        [Header("Top down camera orientation")] 
        [SerializeField] private Vector3 _offetTopPosition = new Vector3(-15.0f, 30.0f, 15.0f);
        [SerializeField] private Vector3 _offsetTopRotation = new Vector3(45.0f, 135.0f, 0.0f);

        [Header("Third person camera orientation")] 
        [SerializeField] private Vector3 _offsetThirdPosition = new Vector3(0.0f, 10.0f, -24.0f);
        [SerializeField] private Vector3 _offsetThirdRotation = new Vector3(0.0f, 0.0f, 0.0f);

        [SerializeField] private float _cameraMoveSpeed = 3.0f;
        [SerializeField] private float _cameraRotateSpeed = 90.0f;
        [Header("For Debug, pls do not set:")]
        [SerializeField] private Transform _topTarget;
        [SerializeField] private Transform _thidrTarget;

        #endregion


        #region Properties

        public Vector3 OffsetTopPosition() => _offetTopPosition;
        public Vector3 OffsetTopRotation() => _offsetTopRotation;
        public Vector3 OffsetThirdPosition() => _offetTopPosition;
        public Vector3 OffsetThirdRotation() => _offsetTopRotation;
        public float   CameraMoveSpeed         => _cameraMoveSpeed;
        public float   CameraRotateSpeed         => _cameraRotateSpeed;
        
        public Transform TopTarget
        {
            get => _topTarget;
            set => _topTarget = value;
        }
        public Transform ThirdTarget
        {
            get => _thidrTarget;
            set => _thidrTarget = value;
        }
        
        #endregion
    }
}