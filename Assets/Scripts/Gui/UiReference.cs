using System;
using Controller;
using CoreComponent;
using Enums;
using Interface;
using Random = UnityEngine.Random;

namespace Gui
{
    [Serializable]
    public sealed class UiReference: IInit, ICleanup
    {
        #region Fields

        private WindowsReference _windows;
        private GeneratorDungeon _generator;
        
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

        public void DefaultState(EnumWindow activeWindow)
        {
            _windows.Hide(EnumWindow.Character);
            _windows.Hide(EnumWindow.Equip);
            _windows.Hide(EnumWindow.Battle);
            _windows.Hide(EnumWindow.Spells);
            _windows.Hide(EnumWindow.Talents);
            
            _windows.Show(activeWindow);
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

        public void SetReference(GeneratorDungeon generatorDungeon)
        {
            _generator = generatorDungeon;
            
            BattlePanel.SeedChange += delegate(int value)
            {
                generatorDungeon.Config.Seed = (uint) value;
            };
            BattlePanel.OnRandomEdit += delegate
            {
                BattlePanel.SeedInputField.text = Random.Range(0, int.MaxValue).ToString();
            };
            BattlePanel.OnGenerateMap += delegate
            {
                _generator.Dungeon.Build();
            };
            BattlePanel.OnIntoBattle += delegate
            {
                
            };
        }
    }
}