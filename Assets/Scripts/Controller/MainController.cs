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
}