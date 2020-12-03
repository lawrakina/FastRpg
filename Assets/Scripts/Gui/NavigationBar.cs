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
        private IReactiveProperty<EnumWindow> _activeWindow;
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

        public void Ctor(IReactiveProperty<EnumWindow> activeWindow)
        {
            _subscriptions = new CompositeDisposable();
            _activeWindow = activeWindow;

            CharToggle.OnValueChangedAsObservable().Subscribe(x =>
            {
                if (x) _activeWindow.Value = EnumWindow.Character;
            }).AddTo(_subscriptions);
            EquipToggle.OnValueChangedAsObservable().Subscribe(x =>
            {
                if (x) _activeWindow.Value = EnumWindow.Equip;
            }).AddTo(_subscriptions);
            BattleToggle.OnValueChangedAsObservable().Subscribe(x =>
            {
                if (x) _activeWindow.Value = EnumWindow.Battle;
            }).AddTo(_subscriptions);
            SpellsToggle.OnValueChangedAsObservable().Subscribe(x =>
            {
                if (x) _activeWindow.Value = EnumWindow.Spells;
            }).AddTo(_subscriptions);
            TalentsToggle.OnValueChangedAsObservable().Subscribe(x =>
            {
                if (x) _activeWindow.Value = EnumWindow.Talents;
            }).AddTo(_subscriptions);

            _subscriptions = new CompositeDisposable();
        }
    }
}