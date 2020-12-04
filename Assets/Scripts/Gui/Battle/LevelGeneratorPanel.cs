using System;
using CoreComponent;
using Interface;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Gui.Battle
{
    public sealed class LevelGeneratorPanel: BasePanel
    {
        #region Fields

        private GeneratorDungeon _generatorDungeon;
        
        [SerializeField] private Button IntoBattleButton;
        [SerializeField] private Button RandomSeedButton;
        [SerializeField] private Text SeedInputField;
        [SerializeField] private Button GenerateMapButton;
        private IBattleInit _battleInit;

        #endregion


        public void SetReference(GeneratorDungeon generatorDungeon)
        {
            _generatorDungeon = generatorDungeon;

            Debug.Log($"1");
            IntoBattleButton.OnPointerClickAsObservable().Subscribe(_ =>
            {
                _battleInit.StartBattle();
            }).AddTo(_subscriptions);
            
            Debug.Log($"2");
            _generatorDungeon.Seed.SubscribeToText(SeedInputField).AddTo(_subscriptions);
            
            Debug.Log($"3");
            var setRandomSeedCommand = new AsyncReactiveCommand();
            setRandomSeedCommand.Subscribe(_ =>
            {
                _generatorDungeon.SetRandomSeed();
                return Observable.Timer(TimeSpan.FromSeconds(1)).AsUnitObservable();
            }).AddTo(_subscriptions);
            setRandomSeedCommand.BindTo(RandomSeedButton).AddTo(_subscriptions);

            Debug.Log($"4");
            GenerateMapButton.OnPointerClickAsObservable().Subscribe(_ =>
            {
                _generatorDungeon.BuildDungeon(); 
            }).AddTo(_subscriptions);
            Debug.Log($"5");
        }


        public void SetReference(IBattleInit battleInit)
        {
            _battleInit = battleInit;
        }
    }
}