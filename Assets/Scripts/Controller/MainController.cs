using Windows;
using CoreComponent;
using Enums;
using Gui;
using UniRx;
using Unit.Player;
using UnityEngine;


namespace Controller
{
    public class MainController : MonoBehaviour
    {
        #region Fields

        private Controllers _controllers;
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private DungeonGeneratorData _generatorData;
        [SerializeField] private UiReference _ui;
        [SerializeField] private WindowsReference _windows;

        private readonly IReactiveProperty<EnumMainWindow> _activeWindow
            = new ReactiveProperty<EnumMainWindow>(EnumMainWindow.Character);

        private readonly IReactiveProperty<EnumBattleWindow> _battleState
            = new ReactiveProperty<EnumBattleWindow>(EnumBattleWindow.DungeonGenerator);
        //todo - прокинуть зависимости в геймконтроллер, UI, WindowS

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _windows.Ctor(_activeWindow, _battleState);
            _ui.Ctor(_activeWindow, _battleState);

            var generatorDungeon = new GeneratorDungeon(_generatorData, _windows.BattleWindow.transform);
            _ui.BattlePanel.LevelGeneratorPanel.SetReference(generatorDungeon);

            var playerFactory = new PlayerFactory(_playerData);
            var player = playerFactory.CreatePlayer();

            var positioningCharInMenuController = new PositioningCharInMenuController();
            positioningCharInMenuController.SetReference(player);
            positioningCharInMenuController.SetReference(generatorDungeon);
            positioningCharInMenuController.AddPlayerPosition(_windows.CharacterWindow.CharacterSpawn.transform,
                EnumMainWindow.Character);
            positioningCharInMenuController.SetReference(_activeWindow, _battleState);
            _ui.BattlePanel.LevelGeneratorPanel.SetReference(positioningCharInMenuController);

            _controllers = new Controllers();
            _controllers.Add(positioningCharInMenuController);

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