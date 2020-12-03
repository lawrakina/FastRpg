using System;
using Windows;
using Enums;
using Interface;
using UniRx;


namespace Gui
{
    [Serializable]
    public sealed class WindowsReference : IInit, ICleanup
    {
        #region Fields

        private IReactiveProperty<EnumWindow> _activeWindow;
        
        public CharacterWindow CharacterWindow;
        public EquipmentWindow EquipmentWindow;
        public BattleWindow BattleWindow;
        public SpellsWindow SpellsWindow;
        public TalentsWindow TalentsWindow;

        #endregion


        public void Ctor(IReactiveProperty<EnumWindow> activeWindow)
        {
            _activeWindow = activeWindow;

            _activeWindow.Subscribe( _ => { ShowOnlyActiveWindow(); });
        }
        
        public void Init()
        {
            CharacterWindow.Init();
            EquipmentWindow.Init();
            BattleWindow.Init();
            SpellsWindow.Init();
            TalentsWindow.Init();
        }

        public void Cleanup()
        {
            CharacterWindow.Cleanup();
            EquipmentWindow.Cleanup();
            BattleWindow.Cleanup();
            SpellsWindow.Cleanup();
            TalentsWindow.Cleanup();
        }


        private void ShowOnlyActiveWindow()
        {
            if(_activeWindow.Value == EnumWindow.Character) CharacterWindow.Show();else CharacterWindow.Hide();
            if(_activeWindow.Value == EnumWindow.Equip) EquipmentWindow.Show();else EquipmentWindow.Hide();
            if(_activeWindow.Value == EnumWindow.Battle) BattleWindow.Show();else BattleWindow.Hide();
            if(_activeWindow.Value == EnumWindow.Spells) SpellsWindow.Show();else SpellsWindow.Hide();
            if(_activeWindow.Value == EnumWindow.Talents) TalentsWindow.Show();else TalentsWindow.Hide();
        }
    }
}