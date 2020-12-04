using System;
using Enums;
using Interface;
using UniRx;


namespace Windows
{
    [Serializable]
    public sealed class WindowsReference : IInit, ICleanup
    {
        #region Fields

        private IReactiveProperty<EnumMainWindow> _activeWindow;
        
        public CharacterWindow CharacterWindow;
        public EquipmentWindow EquipmentWindow;
        public BattleWindow BattleWindow;
        public SpellsWindow SpellsWindow;
        public TalentsWindow TalentsWindow;
        private IReactiveProperty<EnumBattleWindow> _battleState;

        #endregion


        public void Ctor(IReactiveProperty<EnumMainWindow> activeWindow,
            IReactiveProperty<EnumBattleWindow> battleState)
        {
            _battleState = battleState;
            _activeWindow = activeWindow;
            BattleWindow.Ctor(_battleState);
            
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
            if(_activeWindow.Value == EnumMainWindow.Character) CharacterWindow.Show();else CharacterWindow.Hide();
            if(_activeWindow.Value == EnumMainWindow.Equip) EquipmentWindow.Show();else EquipmentWindow.Hide();
            if(_activeWindow.Value == EnumMainWindow.Battle) BattleWindow.Show();else BattleWindow.Hide();
            if(_activeWindow.Value == EnumMainWindow.Spells) SpellsWindow.Show();else SpellsWindow.Hide();
            if(_activeWindow.Value == EnumMainWindow.Talents) TalentsWindow.Show();else TalentsWindow.Hide();
        }
    }
}