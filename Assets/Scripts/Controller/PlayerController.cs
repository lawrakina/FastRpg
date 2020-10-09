using Interface;
using UnityEngine;

namespace Controller
{
    public sealed class PlayerController: IUpdated
    {
        #region Fields

        private Services _services;
        private GameContext _context;

        public PlayerController(Services services, GameContext context)
        {
            _services = services;
            _context = context;
        }

        #endregion

        #region IUdpatable

        public void UpdateTick()
        {
            
        }

        #endregion

        public void Move(Vector2 inputVector)
        {
            
        }
    }
}