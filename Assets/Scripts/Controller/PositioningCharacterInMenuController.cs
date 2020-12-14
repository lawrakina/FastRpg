using System.Collections.Generic;
using CoreComponent;
using Enums;
using Interface;
using UniRx;
using Unit.Player;
using UnityEngine;


namespace Controller
{
    public sealed class PositioningCharacterInMenuController : BaseController
    {
        #region Fields

        private IReactiveProperty<EnumMainWindow> _activeWindow;
        private IReactiveProperty<EnumBattleWindow> _battleState;
        private IPlayerView _player;
        private IGeneratorDungeon _generatorDungeon;
        private Dictionary<EnumMainWindow, Transform> _parentsPositions = new Dictionary<EnumMainWindow, Transform>();

        #endregion


        #region Properties

        public IPlayerView Player
        {
            get => _player;
            set => _player = value;
        }
        public IGeneratorDungeon GeneratorDungeon
        {
            get => _generatorDungeon;
            set => _generatorDungeon = value;
        }

        #endregion
        
        
        public PositioningCharacterInMenuController(IReactiveProperty<EnumMainWindow> activeWindow, IReactiveProperty<EnumBattleWindow> battleState)
        {
            _battleState = battleState;
            _activeWindow = activeWindow;
            
            _activeWindow.Subscribe(_ =>
            {
                if(!_isEnable) return;
                if (!_parentsPositions.ContainsKey(_activeWindow.Value)) return;

                SetPlayerPosition(_parentsPositions[_activeWindow.Value]);
            });
        }



        public void AddPlayerPosition(Transform position, EnumMainWindow mainWindow)
        {
            if (position != null)
                _parentsPositions.Add(mainWindow, position);
        }


        #region SetReference

        // public void SetReference(IPlayerView player)
        // {
        //     _player = player;
        // }
        // public void SetReference(IGeneratorDungeon generatorDungeon)
        // {
        //     _generatorDungeon = generatorDungeon;
        // }
        
        // public void SetReference(IReactiveProperty<EnumMainWindow> activeWindow,
        //     IReactiveProperty<EnumBattleWindow> battleState)
        // {
        //     _battleState = battleState;
        //     _activeWindow = activeWindow;
        //
        //     _activeWindow.Subscribe(_ =>
        //     {
        //         if (!_parentsPositions.ContainsKey(_activeWindow.Value)) return;
        //
        //         SetPlayerPosition(_parentsPositions[_activeWindow.Value]);
        //     });
        // }

        #endregion

        public void StartBattle()
        {
            var playerPosition = _generatorDungeon.GetPlayerPosition();
            Debug.Log($"StartBattle(), playerPosition:{playerPosition}");
            if (playerPosition != null)
            {
                if(!_parentsPositions.ContainsKey(EnumMainWindow.Battle))
                    _parentsPositions.Add(EnumMainWindow.Battle, playerPosition);
                _battleState.Value = EnumBattleWindow.Fight;
                //todo start Battle
                SetPlayerPosition(playerPosition);
            }
        }

        private void SetPlayerPosition(Transform position)
        {
            _player.Transform().SetParent(position);
            _player.Transform().localPosition = Vector3.zero;
            _player.Transform().localRotation = Quaternion.identity;
        }
    }
}