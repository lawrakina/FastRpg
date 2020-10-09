using Controller;

namespace Initializator
{
    public class PlayerComponent
    {
        private PlayerStruct _struct;
        public PlayerComponent(PlayerStruct playerStruct)
        {
            _struct = playerStruct;
        }
    }
}