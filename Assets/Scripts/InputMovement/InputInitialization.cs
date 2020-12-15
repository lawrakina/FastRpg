using Interface;

namespace InputMovement
{
    public sealed class InputInitialization: IInitialization
    {
        #region Fields

        private IUserInputProxy _mobileInputHorizontal;
        private IUserInputProxy _mobileInputVertical;

        #endregion

        public InputInitialization()
        {
            _mobileInputHorizontal = new MobileInputHorizontal();
            _mobileInputVertical = new MobileInputVertical();
        }

        public (IUserInputProxy inputHorizontal, IUserInputProxy inputVertical) GetInput()
        {
            (IUserInputProxy inputHorizontal, IUserInputProxy inputVertical) result = (_mobileInputHorizontal, _mobileInputVertical);
            return result;
        }

        public void On()
        {
            
        }

        public void Off()
        {
            
        }

        public void Initialization()
        {
            
        }
    }
}