﻿using System;
using System.Collections.Generic;
using Controller;
using Enums;
using Interface;
using UniRx;
using Unit.Player;
using UnityEngine;

namespace Gui
{
    [Serializable]
    public sealed class UiReference : IInit, ICleanup
    {
        #region Fields

        private IReactiveProperty<EnumMainWindow> _activeWindow;
        private IReactiveProperty<EnumBattleWindow> _battleState;

        public CharacterPanel CharacterPanel;
        public EquipmentPanel EquipmentPanel;
        public BattlePanel BattlePanel;
        public SpellsPanel SpellsPanel;
        public TalentsPanel TalentsPanel;
        public NavigationBar NavigationBar;
        private IReactiveProperty<EnumCharacterWindow> _charWindow;
        private PrototypePlayerModel _prototypePlayer;

        #endregion


        public void Ctor(IReactiveProperty<EnumMainWindow> activeWindow,
            IReactiveProperty<EnumBattleWindow> battleState,
            IReactiveProperty<EnumCharacterWindow> charWindow, PrototypePlayerModel prototypePlayer)
        {
            _activeWindow = activeWindow;
            _battleState = battleState;
            _charWindow = charWindow;
            _prototypePlayer = prototypePlayer;

            CharacterPanel.Ctor(_charWindow, _prototypePlayer);
            EquipmentPanel.Ctor();
            BattlePanel.Ctor(_battleState);
            SpellsPanel.Ctor();
            TalentsPanel.Ctor();
            NavigationBar.Ctor(_activeWindow, _battleState);

            _activeWindow.Subscribe(_ => { ShowOnlyActivePanel(); });
            _battleState.Subscribe(_ => { ShowBattleOnlyActivePanel(); });
        }

        private void ShowBattleOnlyActivePanel()
        {
            if (_battleState.Value == EnumBattleWindow.DungeonGenerator)
                NavigationBar.Show();
            else NavigationBar.Hide();
        }

        private void ShowOnlyActivePanel()
        {
            // Debug.Log($"ShowOnlyActivePanel:{_activeWindow.Value}");
            // Debug.Log($"CharacterPanel:{CharacterPanel}, BattlePanel:{BattlePanel},{BattlePanel.enabled},{BattlePanel.name}");
            if (_activeWindow.Value == EnumMainWindow.Character)
                CharacterPanel.Show();
            else CharacterPanel.Hide();
            if (_activeWindow.Value == EnumMainWindow.Equip)
                EquipmentPanel.Show();
            else EquipmentPanel.Hide();
            if (_activeWindow.Value == EnumMainWindow.Battle)
                BattlePanel.Show();
            else BattlePanel.Hide();
            if (_activeWindow.Value == EnumMainWindow.Spells)
                SpellsPanel.Show();
            else SpellsPanel.Hide();
            if (_activeWindow.Value == EnumMainWindow.Talents)
                TalentsPanel.Show();
            else TalentsPanel.Hide();
        }

        public void Init()
        {
        }

        public void Init(List<EnumMainWindow> offItemMenu)
        {
            Init();
            CharacterPanel.Init();
            EquipmentPanel.Init();
            BattlePanel.Init();
            SpellsPanel.Init();
            TalentsPanel.Init();
            NavigationBar.Init(offItemMenu);
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