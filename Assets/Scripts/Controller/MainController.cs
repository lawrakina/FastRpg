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

        private readonly List<ILateUpdated> _iLateUpdated = new List<ILateUpdated>();
        private readonly List<IUpdated> _iUpdated = new List<IUpdated>();
        private readonly List<IFixedUpdate> _iFixedUpdated = new List<IFixedUpdate>();
        private readonly List<IEnabled> _iEnableds = new List<IEnabled>();
        private GameContext _gameContext;
        public Services _services;

        [Header("Game Data")] 
        [SerializeField] private GameObject _water;
        [SerializeField] private CameraView _mainCamera;
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private PetData _petData;

        [Header("Game Layers")]
        [SerializeField] private LayerMask _objectsToHideLayer;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private LayerMask _waterLayer;
        [SerializeField] private LayerMask _unitsPlayerAndNpc;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _services = new Services(this);
            _gameContext = new GameContext
            {
                PlayerData = _playerData,
                PetData = _petData,
                GroundLayer = _groundLayer,
                WaterLayer = _waterLayer,
                ObjectsToHideLayer = _objectsToHideLayer,
                LayerUnits = _unitsPlayerAndNpc,
                WaterZone = _water
            };
            
            // new TimeRemainingInitializator(_services);
            new InputInitializator(_services);
            // new PoolInitializator(services, _gameContext);
            new PlayerInitializator(_services, _gameContext);
            new PetInitializator(_services, _gameContext);
            new ThirdCameraInitializator(_services, _gameContext, _mainCamera);
            new ZoneInitializator(_services, _gameContext);
        }

        private void LateUpdate()
        {
            for (var i = 0; i < _iLateUpdated.Count; i++)
            {
                _iLateUpdated[i].LateUpdateTick();
            }
        }

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

        private void OnEnable()
        {
            foreach (var controller in _iEnableds)
            {
                controller.On();
            }
        }

        private void OnDisable()
        {
            foreach (var controller in _iEnableds)
            {
                controller.Off();
            }
        }

        #endregion
        
        
        #region Methods

        public void AddLateUpdated(ILateUpdated controller)
        {
            _iLateUpdated.Add(controller);
        }
        
        public void AddUpdated(IUpdated controller)
        {
            _iUpdated.Add(controller);
        }

        public void AddFixedUpdated(IFixedUpdate controller)
        {
            _iFixedUpdated.Add(controller);
        }

        #endregion

        public void AddEnabledAndDisabled(IEnabled controller)
        {
            _iEnableds.Add(controller);
        }
    }
}