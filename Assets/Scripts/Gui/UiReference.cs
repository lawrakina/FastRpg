using System;
using Enums;
using Interface;
using UniRx;

namespace Gui
{
    [Serializable]
    public sealed class UiReference: IInit, ICleanup
    {
        #region Fields

        private IReactiveProperty<EnumWindow> _activeWindow;
        
        public CharacterPanel CharacterPanel;
        public EquipmentPanel EquipmentPanel;
        public BattlePanel BattlePanel;
        public SpellsPanel SpellsPanel;
        public TalentsPanel TalentsPanel;
        public NavigationBar NavigationBar;

        #endregion
        

        public void Ctor(IReactiveProperty<EnumWindow> activeWindow)
        {
            _activeWindow = activeWindow;

            BattlePanel.Ctor();
            NavigationBar.Ctor(_activeWindow);
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