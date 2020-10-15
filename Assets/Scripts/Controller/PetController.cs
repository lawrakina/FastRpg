using Interface;
using UnityEngine;
using VIew;


namespace Controller
{
    public sealed class PetController : IUpdated
    {
        #region Fields

        private Services _services;
        private GameContext _context;
        private PetView _pet;
        private float startTime;

        #endregion

        
        #region ctor

        public PetController(Services services, GameContext co, PetView view)
        {
            _services = services;
            _context = co;
            _pet = view;
            _pet.Target = _context.PlayerData.PlayerStruct.Player;
            startTime = Time.time;
        }

        #endregion

        public void UpdateTick()
        {
            if(!_pet.isEnable) return;
            _pet.Transform.position = Vector3.Lerp (_pet.transform.position, _pet.Target.transform.position + _pet.OffsetPosition, _pet._moveSpeed * Time.deltaTime);
        }
    }
}