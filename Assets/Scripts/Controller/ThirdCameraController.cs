using Components;
using Interface;
using UnityEngine;


namespace Controller
{
    public sealed class ThirdCameraController: IUpdated
    {
        #region Fields
    
        private CameraComponent _cameraComponent;
        private Transform _target;
    
        #endregion
    
        public ThirdCameraController(CameraComponent camera, GameContext context)
        {
            _cameraComponent = camera;
            _target = context.PlayerData.PlayerStruct.Player.transform;
        }
        
        #region UnityMethods
        
        // public void SearchPlayer()
        // {
        //     _target = Object.FindObjectOfType<PlayerComponent>().Transform;
        //     if(_target)
        //         _cameraComponent.SetTargetTransform(_target);
        // }
    
        #endregion
    
        public void UpdateTick()
        {
            _cameraComponent.UpdateTick();
        }
    }
}