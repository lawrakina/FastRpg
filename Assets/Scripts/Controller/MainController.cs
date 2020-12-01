using DungeonArchitect;
using DungeonArchitect.Builders.GridFlow;
using Enums;
using Gui;
using UnityEngine;


namespace Controller
{
    public class MainController : MonoBehaviour
    {
        #region Fields
        
        // [SerializeField] private Canvas _canvasRoot;
        // [SerializeField] private GuiData _guiData;
        [SerializeField] private UiReference _ui;
        [SerializeField] private WindowsReference _windows;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _windows.Init();
            _ui.Ctor(_windows);
            _ui.Init();

            var generator = new GeneratorDungeon
            {
                Dungeon = _windows.BattleWindow.DungeonGenerator,
                Config = _windows.BattleWindow.DungeonConfig,
                Builder = _windows.BattleWindow.DungeonBuilder
            };

            _ui.SetReference(generator);
            
            
            
            _ui.DefaultState(EnumWindow.Character);

            // var guiInitialization = new GuiInitialization(_canvasRoot, _guiData);
            // var guiCommands = new GuiCommands(guiInitialization);
            //
            // guiCommands.Init();
            //
            // guiCommands.OnCharacterPanelShow += () => { Debug.Log($"ShowPanelCharacter"); };
            // guiCommands.OnBattlePanelShow += () => { Debug.Log($"ShowPanelBattle"); };
        }

        #endregion
    }

    public sealed class GeneratorDungeon
    {
        public Dungeon Dungeon { get; set; }
        public GridFlowDungeonConfig Config { get; set; }
        public GridFlowDungeonBuilder Builder { get; set; }
    }
}