using System;
using Enums;
using Interface;


namespace Gui
{
    [Serializable]
    public sealed class UiReference: IInit, ICleanup
    {
        #region Fields

        private WindowsReference _windows;
        
        public CharacterPanel CharacterPanel;
        public EquipmentPanel EquipmentPanel;
        public BattlePanel BattlePanel;
        public SpellsPanel SpellsPanel;
        public TalentsPanel TalentsPanel;
        public NavigationBar NavigationBar;

        #endregion

        
        public void Ctor(WindowsReference windows)
        {
            _windows = windows;
            
            NavigationBar.Ctor(_windows);
        }

        public void DefaultState(EnumWindow character)
        {
            _windows.Hide(EnumWindow.Character);
            _windows.Hide(EnumWindow.Equip);
            _windows.Hide(EnumWindow.Battle);
            _windows.Hide(EnumWindow.Spells);
            _windows.Hide(EnumWindow.Talents);
            
            _windows.Show(character);
        }
        
        public void Init()
        {
            CharacterPanel.Init();
            EquipmentPanel.Init();
            BattlePanel.Init();
            SpellsPanel.Init();
            TalentsPanel.Init();
            NavigationBar.Init();
        }

        public void Cleanup()
        {
            CharacterPanel.Cleanup();
            EquipmentPanel.Cleanup();
            BattlePanel.Cleanup();
            SpellsPanel.Cleanup();
            TalentsPanel.Cleanup();
            NavigationBar.Cleanup();
        }
    }
}