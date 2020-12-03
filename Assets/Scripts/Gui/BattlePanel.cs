using System;
using CoreComponent;
using Interface;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Gui
{
    [Serializable]
    public sealed class BattlePanel : MonoBehaviour, IInit, ICleanup
    {
        #region Fields

        private CompositeDisposable _subscriptions;
        private GeneratorDungeon _generatorDungeon;
        
        [SerializeField] private Button IntoBattleButton;
        [SerializeField] private Button RandomSeedButton;
        [SerializeField] private Text SeedInputField;
        [SerializeField] private Button GenerateMapButton;

        #endregion


        public void Ctor()
        {
            _subscriptions = new CompositeDisposable();
        }
        
        public void Init()
        {
            
        }

        public void SetReference(GeneratorDungeon generatorDungeon)
        {
            _generatorDungeon = generatorDungeon;

            _generatorDungeon.Seed.SubscribeToText(SeedInputField).AddTo(_subscriptions);
            
            var setRandomSeedCommand = new AsyncReactiveCommand();
            setRandomSeedCommand.Subscribe(_ =>
            {
                _generatorDungeon.DestroyDungeon();
                _generatorDungeon.Seed.Value = (uint) Random.Range(0, int.MaxValue);
                return Observable.Timer(TimeSpan.FromSeconds(1)).AsUnitObservable();
            }).AddTo(_subscriptions);
            setRandomSeedCommand.BindTo(RandomSeedButton).AddTo(_subscriptions);

            GenerateMapButton.OnPointerClickAsObservable().Subscribe(_ =>
            {
                _generatorDungeon.BuildDungeon(); 
            }).AddTo(_subscriptions);
        }

        public void Cleanup()
        {
            _subscriptions?.Dispose();
        }
    }
}