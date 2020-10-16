using System;
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
        private readonly List<IFixedUpdate> _iFixedUpdated = new List<IFixedUpdate>();
        private GameContext _gameContext;
        private Services _services;
        [SerializeField] private CameraView _mainCamera;
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private PetData _petData;

        [SerializeField] private LayerMask _groundLayer;

        #endregion
        
        
        private void Start()
        {
            _services = new Services(this);
            _gameContext = new GameContext
            {
                PlayerData = _playerData,
                PetData = _petData,
                GroundLayer = _groundLayer
            };
            
            // new TimeRemainingInitializator(_services);
            new InputInitializator(_services);
            // new PoolInitializator(services, _gameContext);
            new PlayerInitializator(_services, _gameContext);
            new PetInitializator(_services, _gameContext);
            new ThirdCameraInitializator(_services, _gameContext, _mainCamera);
        }
        
        
        #region Methods
        
        private void Update()
        {
            for (var i = 0; i < _iUpdated.Count; i++)
            {
                _iUpdated[i].UpdateTick();
            }
        }

        private void FixedUpdate()
        {
            for (var i = 0; i < _iFixedUpdated.Count; i++)
            {
                _iFixedUpdated[i].FixedUpdateTick();
            }
        }

        public void AddUpdated(IUpdated iUpdated)
        {
            _iUpdated.Add(iUpdated);
        }

        public void AddFixedUpdated(IFixedUpdate iFixedUpdate)
        {
            _iFixedUpdated.Add(iFixedUpdate);
        }

        #endregion
    }
}