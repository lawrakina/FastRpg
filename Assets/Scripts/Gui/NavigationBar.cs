using System;
using Enums;
using Interface;
using UnityEngine;
using UnityEngine.UI;


namespace Gui
{
    [Serializable]
    public sealed class NavigationBar : MonoBehaviour, IInit, ICleanup
    {
        private WindowsReference _windows;
        
        public Toggle CharToggle;
        public Toggle EquipToggle;
        public Toggle BattleToggle;
        public Toggle SpellsToggle;
        public Toggle TalentsToggle;

        public Action<bool> OnChar;
        public Action<bool> OnEquip;
        public Action<bool> OnBattle;
        public Action<bool> OnSpells;
        public Action<bool> OnTalents;

        public void Ctor(WindowsReference windows)
        {
            _windows = windows;//исключаем замыкания
            
            //todo изучить mvvm, уйти от монолита
            OnChar += delegate(bool b)
            { if (b) _windows.Show(EnumWindow.Character);
                else _windows.Hide(EnumWindow.Character); };
            OnEquip += delegate(bool b)
            { if (b) _windows.Show(EnumWindow.Equip);
                else _windows.Hide(EnumWindow.Equip); };
            OnBattle += delegate(bool b)
            { if (b) _windows.Show(EnumWindow.Battle);
                else _windows.Hide(EnumWindow.Battle); };
            OnSpells += delegate(bool b)
            { if (b) _windows.Show(EnumWindow.Spells);
                else _windows.Hide(EnumWindow.Spells); };
            OnTalents += delegate(bool b)
            { if (b) _windows.Show(EnumWindow.Talents);
                else _windows.Hide(EnumWindow.Talents); };
        }
        
        public void Init()
        {
            CharToggle.onValueChanged.AddListener(delegate { OnChar?.Invoke(CharToggle.isOn); });
            EquipToggle.onValueChanged.AddListener(delegate { OnEquip?.Invoke(EquipToggle.isOn); });
            BattleToggle.onValueChanged.AddListener(delegate { OnBattle?.Invoke(BattleToggle.isOn); });
            SpellsToggle.onValueChanged.AddListener(delegate { OnSpells?.Invoke(SpellsToggle.isOn); });
            TalentsToggle.onValueChanged.AddListener(delegate { OnTalents?.Invoke(TalentsToggle.isOn); });
        }

        public void Cleanup()
        {
            CharToggle.onValueChanged.RemoveAllListeners();
            EquipToggle.onValueChanged.RemoveAllListeners();
            BattleToggle.onValueChanged.RemoveAllListeners();
            SpellsToggle.onValueChanged.RemoveAllListeners();
            TalentsToggle.onValueChanged.RemoveAllListeners();
        }

    }
}