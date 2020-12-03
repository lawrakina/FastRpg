using System;
using Enums;
using Gui.Windows;
using Interface;


namespace Gui
{
    [Serializable]
    public sealed class WindowsReference : IInit, IWindows, ICleanup
    {
        #region Fields

        public CharacterWindow CharacterWindow;
        public EquipmentWindow EquipmentWindow;
        public BattleWindow BattleWindow;
        public SpellsWindow SpellsWindow;
        public TalentsWindow TalentsWindow;

        #endregion

        
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

        
        #region IWindows

        public void Show(EnumWindow enumWindow)
        {
            //todo изучить mvvm, уйти от монолита
            switch (enumWindow)
            {
                case EnumWindow.Character:
                    CharacterWindow.Show();
                    break;
                case EnumWindow.Equip:
                    EquipmentWindow.Show();
                    break;
                case EnumWindow.Battle:
                    BattleWindow.Show();
                    break;
                case EnumWindow.Spells:
                    SpellsWindow.Show();
                    break;
                case EnumWindow.Talents:
                    TalentsWindow.Show();
                    break;
                default:
                    break;
            }
        }

        public void Hide(EnumWindow enumWindow)
        {
            //todo изучить mvvm, уйти от монолита
            switch (enumWindow)
            {
                case EnumWindow.Character:
                    CharacterWindow.Hide();
                    break;
                case EnumWindow.Equip:
                    EquipmentWindow.Hide();
                    break;
                case EnumWindow.Battle:
                    BattleWindow.Hide();
                    break;
                case EnumWindow.Spells:
                    SpellsWindow.Hide();
                    break;
                case EnumWindow.Talents:
                    TalentsWindow.Hide();
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}