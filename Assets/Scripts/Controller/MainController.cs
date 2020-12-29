using System.Collections.Generic;
using Windows;
using Battle;
using CoreComponent;
using Data;
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

        [Header("Game Layers")]
        [SerializeField]
        private LayerMask _groundLayer;

        [Header("Game Data")]
        [SerializeField]
        private PlayerData _playerData;

        [SerializeField]
        private CharacterData _characterData;

        [SerializeField]
        private DungeonGeneratorData _generatorData;

        [Header("Ui & Windows")]
        [SerializeField]
        private UiReference _ui;

        [SerializeField]
        private WindowsReference _windows;

        [Header("Active Panel and Window at the Start")]
        [SerializeField]
        private EnumMainWindow _activePanelAndWindow;

        [Header("Type of camera and char control")]
        [SerializeField]
        private readonly EnumFightCamera _fightCameraType = EnumFightCamera.ThirdPersonView;

        private IReactiveProperty<EnumMainWindow> _activeWindow;
        private IReactiveProperty<EnumCharacterWindow> _charWindow;
        private IReactiveProperty<EnumBattleWindow> _battleState;
        private IReactiveProperty<EnumFightCamera> _typeCameraAndCharControl;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            LayerManager.GroundLayer = _groundLayer;

            //UI & Windows
            _activeWindow = new ReactiveProperty<EnumMainWindow>();
            _charWindow = new ReactiveProperty<EnumCharacterWindow>(EnumCharacterWindow.ListCharacters);
            _battleState = new ReactiveProperty<EnumBattleWindow>(EnumBattleWindow.DungeonGenerator);

            _activeWindow.Subscribe(_ => { Dbg.Log(_activeWindow.Value); });
            _charWindow.Subscribe(_ => { Dbg.Log(_charWindow.Value); });
            _battleState.Subscribe(_ => { Dbg.Log(_battleState.Value); });

            var customizerCharacter = new CustomizerCharacter(_characterData);
            var playerFactory = new PlayerFactory(customizerCharacter, _characterData);
            var listOfCharactersController = new ListOfCharactersController(_playerData, playerFactory);
            var player = listOfCharactersController.CurrentCharacter.Value;
















            // var playerFactory = new PlayerFactory();
            // var customizingCharacter = new CustomizingCharacter(_characterData);
            // var listCharactersManager = new ListCharactersManager(playerFactory, customizingCharacter, _playerData);
            // var player = listCharactersManager.CurrentChar.Value;

            // 1-вый запуск чаров нет
            //     взять префаб фентезиЧара
            //         если в SO нет записей для чего какая вещь - заполнить
            //         если есть - прочитать из сохранения и создать первого чара
            //         если чаров несколько то создать и добавить в менеджер списка чаров.
            //     если никого нет то
            //     создать пустого чара,
            //         все поля редактирования персонажа привязать к прототипу
            //     когда сохраняем прототип в персонажа
            //         
            //  если записи есть
            //         создать нового персонажа по сохраненным настройкам
            //     создать всех персонажей

            //create ui & windows
            _windows.Ctor(_activeWindow, _battleState);
            _ui.Ctor(_activeWindow, _battleState, _charWindow, listOfCharactersController);

            //ввод с экранного джойстика
            var inputInitialization = new InputInitialization();

            //generator levels
            var generatorDungeon = new GeneratorDungeon(_generatorData, _windows.BattleWindow.Content.transform);
            _ui.BattlePanel.LevelGeneratorPanel.SetReference(generatorDungeon);

            var fightCameraFactory = new CameraFactory();
            // камера используется в рендере gui и сцены - todo все в SO и префабы
            var fightCamera = fightCameraFactory.CreateCamera(_windows.BattleWindow.Camera);

            //Positioning character in menu
            var positioningCharInMenuController = new PositioningCharacterInMenuController(_activeWindow, _battleState);
            positioningCharInMenuController.Player = player;
            positioningCharInMenuController.GeneratorDungeon = generatorDungeon;
            positioningCharInMenuController.AddPlayerPosition(
                _windows.CharacterWindow.CharacterSpawn(), EnumMainWindow.Character);
            positioningCharInMenuController.AddPlayerPosition(
                _windows.EquipmentWindow.CharacterSpawn(), EnumMainWindow.Equip);
            positioningCharInMenuController.AddPlayerPosition(
                generatorDungeon.GetPlayerPosition(), EnumMainWindow.Battle);
            positioningCharInMenuController.AddPlayerPosition(
                _windows.SpellsWindow.CharacterSpawn(), EnumMainWindow.Spells);
            positioningCharInMenuController.AddPlayerPosition(
                _windows.TalentsWindow.CharacterSpawn(), EnumMainWindow.Talents);

            var battleInitialization = new BattleInitialization(generatorDungeon, _battleState, _activeWindow, player);
            battleInitialization.Dungeon = generatorDungeon.Dungeon();
            _ui.BattlePanel.LevelGeneratorPanel.SetReference(battleInitialization);

            _typeCameraAndCharControl = new ReactiveProperty<EnumFightCamera>(_fightCameraType);
            var battleCameraController =
                new FightCameraController(_battleState, player, fightCamera, _typeCameraAndCharControl);
            var battlePlayerMoveController =
                new MovePlayerController(_battleState, inputInitialization.GetInput(), player,
                    _typeCameraAndCharControl);
            var inputController = new InputController(inputInitialization.GetInput());

            _controllers = new Controllers();
            // _controllers.Add(customizingCharacter);
            _controllers.Add(positioningCharInMenuController);
            _controllers.Add(battleCameraController);
            _controllers.Add(inputInitialization);
            _controllers.Add(battlePlayerMoveController);
            _controllers.Add(inputController);

            var offItemMenu = new List<EnumMainWindow>();
            offItemMenu.Add(EnumMainWindow.Equip);
            offItemMenu.Add(EnumMainWindow.Spells);
            offItemMenu.Add(EnumMainWindow.Talents);
            _ui.Init(offItemMenu);
            _windows.Init();
            _controllers.Initialization();
            _activeWindow.Value = _activePanelAndWindow;
        }


        #region Methods

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

        #endregion
    }
}