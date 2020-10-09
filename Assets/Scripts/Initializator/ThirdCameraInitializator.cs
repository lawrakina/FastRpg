using Components;
using Controller;
using UnityEngine;


namespace Initializator
{
    public class ThirdCameraInitializator
    {
        public ThirdCameraInitializator(Services services, GameContext context)
        {   
            var camera = Object.FindObjectOfType<CameraComponent>();
            services.MainController.AddUpdated(new ThirdCameraController(camera, context));
        }
    }
}