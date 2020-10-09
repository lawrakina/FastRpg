using Controller.TimeRemaining;
using Interface;
using Model;
using UnityEngine;


namespace MVC.Controller
{
    public sealed class Controllers : IInitialization
    {
        #region Fields

        private readonly IExecute[] _executeControllers;
        private readonly IFixedExecute[] _fixedExecutesControllers;

        #endregion


        #region Properties

        public IExecute this[int index] => _executeControllers[index];
        public IFixedExecute[] FixedExecute => _fixedExecutesControllers;
        public int Length => _executeControllers.Length;
        public int FixedLenght => _fixedExecutesControllers.Length;

        #endregion


        public Controllers()
        {
            // IMotor motor = new UnitMotor(ServiceLocatorMonoBehaviour.GetService<CharacterController>());
            ServiceLocator.SetService(new TimeRemainingController());
            ServiceLocator.SetService(new PlayerController());
            ServiceLocator.SetService(new ThirdCameraController());
            // ServiceLocator.SetService(new FlashLightController());
            ServiceLocator.SetService(new InputController());
            // ServiceLocator.SetService(new SelectionController());
            // ServiceLocator.SetService(new WeaponController());
            ServiceLocator.SetService(new InventoryController());
            ServiceLocator.SetService(new BotController());
            ServiceLocator.SetService(new PoolController());
            ServiceLocator.SetService(new AudioController());

            _executeControllers = new IExecute[5];
            _executeControllers[0] = ServiceLocator.Resolve<TimeRemainingController>();
            _executeControllers[1] = ServiceLocator.Resolve<PlayerController>();
            _executeControllers[2] = ServiceLocator.Resolve<InputController>();
            _executeControllers[3] = ServiceLocator.Resolve<ThirdCameraController>();
            _executeControllers[4] = ServiceLocator.Resolve<BotController>();
            // _executeControllers[3] = ServiceLocator.Resolve<FlashLightController>();
            // _executeControllers[4] = ServiceLocator.Resolve<SelectionController>();
            
            _fixedExecutesControllers = new IFixedExecute[2];
            _fixedExecutesControllers[0] = ServiceLocator.Resolve<TimeRemainingController>();
            _fixedExecutesControllers[1] = ServiceLocator.Resolve<PlayerController>();
        }


        #region IInitialization

        public void Initialization()
        {
            foreach (var controller in _executeControllers)
            {
                if (controller is IInitialization initialization)
                {
                    initialization.Initialization();
                }
            }
            
            Transform gameControllerTransform = Object.FindObjectOfType<GameController>().transform;

            ServiceLocator.Resolve<InventoryController>().Initialization();
            ServiceLocator.Resolve<InputController>().On();
            ServiceLocator.Resolve<PlayerController>().Initialization();
            ServiceLocator.Resolve<BotController>().On();
            ServiceLocator.Resolve<PoolController>().Init(gameControllerTransform);
            ServiceLocator.Resolve<AudioController>().Initialization(gameControllerTransform);
            // ServiceLocator.Resolve<SelectionController>().On();
            // ServiceLocator.Resolve<PlayerController>().On();
        }

        #endregion
    }
}