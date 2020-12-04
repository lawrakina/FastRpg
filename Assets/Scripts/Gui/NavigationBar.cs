using System;
using Enums;
using Interface;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace Gui
{
    [Serializable]
    public sealed class NavigationBar : MonoBehaviour, IInit, ICleanup
    {
        private IReactiveProperty<EnumMainWindow> _activeWindow;
        private IReactiveProperty<EnumBattleWindow> _battleState;
        private CompositeDisposable _subscriptions;

        public Toggle CharToggle;
        public Toggle EquipToggle;
        public Toggle BattleToggle;
        public Toggle SpellsToggle;
        public Toggle TalentsToggle;


        public void Init()
        {
        }

        public void Cleanup()
        {
            _subscriptions?.Dispose();
        }

        public void Ctor(IReactiveProperty<EnumMainWindow> activeWindow,
            IReactiveProperty<EnumBattleWindow> battleState)
        {
            _subscriptions = new CompositeDisposable();
            _battleState = battleState;
            _activeWindow = activeWindow;

            CharToggle.OnValueChangedAsObservable().Subscribe(x =>
            {
                if (x) _activeWindow.Value = EnumMainWindow.Character;
            }).AddTo(_subscriptions);
            EquipToggle.OnValueChangedAsObservable().Subscribe(x =>
            {
                if (x) _activeWindow.Value = EnumMainWindow.Equip;
            }).AddTo(_subscriptions);
            BattleToggle.OnValueChangedAsObservable().Subscribe(x =>
            {
                if (x)
                {
                    _activeWindow.Value = EnumMainWindow.Battle;
                    _battleState.Value = EnumBattleWindow.DungeonGenerator;
                }
            }).AddTo(_subscriptions);
            SpellsToggle.OnValueChangedAsObservable().Subscribe(x =>
            {
                if (x) _activeWindow.Value = EnumMainWindow.Spells;
            }).AddTo(_subscriptions);
            TalentsToggle.OnValueChangedAsObservable().Subscribe(x =>
            {
                if (x) _activeWindow.Value = EnumMainWindow.Talents;
            }).AddTo(_subscriptions);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}