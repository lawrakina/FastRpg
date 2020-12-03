using CoreComponent;
using Enums;
using Gui;
using Interface;
using Unit.Player;
using UnityEngine;


namespace Controller
{
    public class MainController : MonoBehaviour
    {
        #region Fields

        private Controllers _controllers;
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private UiReference _ui;
        [SerializeField] private WindowsReference _windows;
        

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _windows.Init();
            _ui.Ctor(_windows);
            _ui.Init();

            var generatorDungeon = new GeneratorDungeon
            {
                Dungeon = _windows.BattleWindow.DungeonGenerator,
                Config = _windows.BattleWindow.DungeonConfig,
                Builder = _windows.BattleWindow.DungeonBuilder
            };
            _ui.SetReference(generatorDungeon);
            
            var gameController = new GameController();
            
            _controllers = new Controllers();
            _controllers.Add(gameController);

            var playerFactory = new PlayerFactory(_playerData);
            var player = playerFactory.CreatePlayer();

            _controllers.Initialization();
            _ui.DefaultState(EnumWindow.Character);
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

    public sealed class GameController: IInitialization
    {
        public void Initialization()
        {
            
        }
    }
}