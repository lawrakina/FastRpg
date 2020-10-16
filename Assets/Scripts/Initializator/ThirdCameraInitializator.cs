using Controller;
using VIew;


namespace Initializator
{
    public class ThirdCameraInitializator
    {
        public ThirdCameraInitializator(Services services, GameContext context, CameraView mainCamera)
        {   
            // var camera = Object.FindObjectOfType<CameraComponent>();
            services.ThirdCameraController = new ThirdCameraController(mainCamera, context);
            services.MainController.AddUpdated(services.ThirdCameraController);
            services.MainController.AddEnabledAndDisabled(services.ThirdCameraController);
        }
    }
}