using System.Collections.Generic;
using Data;
using Initializator;
using Interface;
using UnityEngine;
using VIew;


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
        [SerializeField] private CameraView _mainCamera;
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private PetData _petData;
        

        #endregion
        
        
        private void Start()
        {
            _services = new Services(this);
            _gameContext = new GameContext
            {
                PlayerData = _playerData,
                PetData = _petData
            };
            
            // new TimeRemainingInitializator(_services);
            new InputInitializator(_services);
            // new PoolInitializator(services, _gameContext);
            new PlayerInitializator(_services, _gameContext);
            new PetInitializator(_services, _gameContext);
            new ThirdCameraInitializator(_services, _gameContext, _mainCamera);
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