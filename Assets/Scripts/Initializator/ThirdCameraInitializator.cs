using Controller;
using VIew;


namespace Initializator
{
    public class ThirdCameraInitializator
    {
        public ThirdCameraInitializator(Services services, GameContext context, CameraView mainCamera)
        {   
            // var camera = Object.FindObjectOfType<CameraComponent>();
            services.MainController.AddUpdated(new ThirdCameraController(mainCamera, context));
        }
    }
}