using System;
using Gui;
using Interface;


namespace Controller
{
    public sealed class GuiCommands: ICleanup
    {
        #region Fields

        private readonly GuiInitialization _gui;

        #endregion


        #region Actions

        public Action OnCharacterPanelShow = delegate {  };
        public Action OnEquipmentPanelShow = delegate {  };
        public Action OnBattlePanelShow = delegate {  };
        public Action OnSpellsPanelShow = delegate {  };
        public Action OnTalentsPanelShow = delegate {  };

        #endregion
        
        
        public GuiCommands(GuiInitialization gui)
        {
            _gui = gui;
        }

        //todo изучить и переделать в mvvm
        public void Init()
        {
            //Navigation commands
            _gui.NavigationBar.CharToggle.onValueChanged.AddListener(delegate
            {
                HideAllPanels();
                OnCharacterPanelShow?.Invoke();
                _gui.CharacterPanel.enabled = true;
            });
            _gui.NavigationBar.EquipToggle.onValueChanged.AddListener(delegate
            {
                HideAllPanels();
                OnEquipmentPanelShow?.Invoke();
                _gui.EquipmentPanel.enabled = true;
            });
            _gui.NavigationBar.BattleToggle.onValueChanged.AddListener(delegate
            {
                HideAllPanels();
                OnBattlePanelShow?.Invoke();
                _gui.BattlePanel.enabled = true;
            });
            _gui.NavigationBar.SpellsToggle.onValueChanged.AddListener(delegate
            {
                HideAllPanels();
                OnSpellsPanelShow?.Invoke();
                _gui.SpellsPanel.enabled = true;
            });
            _gui.NavigationBar.TalentsToggle.onValueChanged.AddListener(delegate
            {
                HideAllPanels();
                OnTalentsPanelShow?.Invoke();
                _gui.TalentsPanel.enabled = true;
            });
            
        }

        private void HideAllPanels()
        {
            _gui.CharacterPanel.enabled = false;
            _gui.EquipmentPanel.enabled = false;
            _gui.BattlePanel.enabled = false;
            _gui.SpellsPanel.enabled = false;
            _gui.TalentsPanel.enabled = false;
        }

        public void Cleanup()
        {
            _gui.NavigationBar.CharToggle.onValueChanged.RemoveAllListeners();
            _gui.NavigationBar.EquipToggle.onValueChanged.RemoveAllListeners();
            _gui.NavigationBar.BattleToggle.onValueChanged.RemoveAllListeners();
            _gui.NavigationBar.SpellsToggle.onValueChanged.RemoveAllListeners();
            _gui.NavigationBar.TalentsToggle.onValueChanged.RemoveAllListeners();
        }
    }
}