using Windows;
using Data;
using Dungeon;
using Enums;
using Extension;
using Gui;
using InputMovement;
using UniRx;
using Unit.Cameras;
using Unit.Player;
using UnityEngine;


namespace Controller
{
    public class MainController : MonoBehaviour
    {
        #region Fields

        private Controllers _controllers;

        [Header("Game Layers")] [SerializeField]
        private LayerMask _groundLayer;

        [Header("Game Data")] [SerializeField] private PlayerData _playerData;
        [SerializeField] private DungeonGeneratorData _generatorData;

        [Header("Ui & Windows")] [SerializeField]
        private UiReference _ui;

        [SerializeField] private WindowsReference _windows;

        [Header("Active Panel and Window at the Start")] [SerializeField]
        private EnumMainWindow _activePanelAndWindow = EnumMainWindow.Character;

        private IReactiveProperty<EnumMainWindow> _activeWindow;
        private IReactiveProperty<EnumBattleWindow> _battleState;

        [Header("Type of camera and char control")] [SerializeField]
        private EnumFightCamera _fightCameraType = EnumFightCamera.ThirdPersonView;
        private IReactiveProperty<EnumFightCamera> _typeCameraAndCharControl;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            LayerManager.GroundLayer = _groundLayer;
            
            _activeWindow = new ReactiveProperty<EnumMainWindow>(_activePanelAndWindow);
            _typeCameraAndCharControl = new ReactiveProperty<EnumFightCamera>(_fightCameraType);
            _battleState = new ReactiveProperty<EnumBattleWindow>(EnumBattleWindow.DungeonGenerator);

            _windows.Ctor(_activeWindow, _battleState);
            _ui.Ctor(_activeWindow, _battleState);

            var inputInitialization = new InputInitialization();
            
            var generatorDungeon = new GeneratorDungeon(_generatorData, _windows.BattleWindow.transform);
            _ui.BattlePanel.LevelGeneratorPanel.SetReference(generatorDungeon);

            var playerFactory = new PlayerFactory(_playerData);
            var player = playerFactory.CreatePlayer();
            var fightCameraFactory = new CameraFactory();
            var fightCamera = fightCameraFactory.CreateCamera(_windows.BattleWindow.Camera);

            var positioningCharInMenuController = new PositioningCharInMenuController();
            positioningCharInMenuController.SetReference(player);
            positioningCharInMenuController.SetReference(generatorDungeon);
            positioningCharInMenuController.AddPlayerPosition(_windows.CharacterWindow.CharacterSpawn.transform,
                EnumMainWindow.Character);
            positioningCharInMenuController.SetReference(_activeWindow, _battleState);
            _ui.BattlePanel.LevelGeneratorPanel.SetReference(positioningCharInMenuController);

            var battleCameraController =
                new FightCameraController(_battleState, player, fightCamera, _typeCameraAndCharControl);
            var battlePlayerMoveController =
                new MovePlayerController(_battleState,inputInitialization.GetInput(), player, _typeCameraAndCharControl);
            var inputController = new InputController(inputInitialization.GetInput());

            _controllers = new Controllers();
            _controllers.Add(positioningCharInMenuController);
            _controllers.Add(battleCameraController);
            _controllers.Add(inputInitialization);
            _controllers.Add(battlePlayerMoveController);
            _controllers.Add(inputController);

            _ui.Init();
            _windows.Init();
            _controllers.Initialization();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _controllers.Execute(deltaTime);
        }

        private void LateUpdate()
        {
            var deltaTime = Time.deltaTime;
            _controllers.LateExecute(deltaTime);
        }

        private void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;
            _controllers.FixedExecute(deltaTime);
        }

        private void OnDestroy()
        {
            _controllers.Cleanup();
        }

        #endregion
    }
}