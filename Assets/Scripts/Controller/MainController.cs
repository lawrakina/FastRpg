using CoreComponent;
using Enums;
using Gui;
using Interface;
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

        private IReactiveProperty<EnumWindow> _activeWindow 
            = new ReactiveProperty<EnumWindow>(EnumWindow.Character);

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _windows.Ctor(_activeWindow);
            _ui.Ctor(_activeWindow);

            var generatorDungeon = new GeneratorDungeon(_generatorData, _windows.BattleWindow.transform);
            _ui.BattlePanel.SetReference(generatorDungeon);
            
            var gameController = new GameController();

            _controllers = new Controllers();
            _controllers.Add(gameController);

            // var playerFactory = new PlayerFactory(_playerData);
            // var player = playerFactory.CreatePlayer();

            
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


    public sealed class GameController : IInitialization
    {
        public void Initialization()
        {
        }
    }
}