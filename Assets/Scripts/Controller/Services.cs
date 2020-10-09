namespace Controller
{
    public class Services
    {
        #region Properties

        public MainController MainController { get; }
        // public PoolController PoolController { get; set; }
        public InputController InputController { get; set; }
        public PlayerController PlayerController { get; set; }
        public ThirdCameraController ThirdCameraController { get; set; }
        // public InventoryController InventoryController { get; set; }

        #endregion

        
        #region ctor

        public Services(MainController mainController)
        {
            MainController = mainController;
        }

        #endregion
    }
}