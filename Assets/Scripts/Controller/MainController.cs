using System.Collections.Generic;
using Initializator;
using Interface;
using UnityEngine;


namespace Controller
{
    // public sealed class BaseController: IUpdated
    // {
    //     private BaseModel _model;
    //     private BaseView _view;
    //
    //     public BaseController(BaseModel model, BaseView view)
    //     {
    //         _model
    //             
    //     }
    // }
    
    
    public sealed class MainController : MonoBehaviour
    {
        #region Fields

        private readonly List<IUpdated> _iUpdated = new List<IUpdated>();
        private GameContext _gameContext;
        private Services _services;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private PlayerData _playerData;
        

        #endregion
        
        
        private void Start()
        {
            _services = new Services(this);
            _gameContext = new GameContext
            {
                PlayerData = _playerData,
            };
            // _gameContext.Camera = _mainCamera;
            
            // new TimeRemainingInitializator(_services);
            // new InputInitializator(services);
            // new PoolInitializator(services, _gameContext);
            // new PlayerInitializator(_services, _gameContext);
            new ThirdCameraInitializator(_services, _gameContext);
        }
        
        private void Update()
        {
            for (var i = 0; i < _iUpdated.Count; i++)
            {
                _iUpdated[i].UpdateTick();
            }
        }
        
        #region Methods

        public void AddUpdated(IUpdated iUpdated)
        {
            _iUpdated.Add(iUpdated);
        }

        #endregion
    }
}