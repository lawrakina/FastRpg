using System.Collections.Generic;
using Dungeon;
using Enums;
using Interface;
using UniRx;
using Unit.Player;
using UnityEngine;


namespace Controller
{
    public sealed class PositioningCharInMenuController : BaseController, IBattleInit
    {
        private IPlayerView _player;
        private IReactiveProperty<EnumMainWindow> _activeWindow;
        private Dictionary<EnumMainWindow, Transform> _parentsPositions = new Dictionary<EnumMainWindow, Transform>();
        private IGeneratorDungeon _generatorDungeon;
        private IReactiveProperty<EnumBattleWindow> _battleState;

        public void AddPlayerPosition(Transform position, EnumMainWindow mainWindow)
        {
            if (position != null)
                _parentsPositions.Add(mainWindow, position);
        }


        #region SetReference

        public void SetReference(IPlayerView player)
        {
            _player = player;
        }
        public void SetReference(IGeneratorDungeon generatorDungeon)
        {
            _generatorDungeon = generatorDungeon;
        }
        
        public void SetReference(IReactiveProperty<EnumMainWindow> activeWindow,
            IReactiveProperty<EnumBattleWindow> battleState)
        {
            _battleState = battleState;
            _activeWindow = activeWindow;

            _activeWindow.Subscribe(_ =>
            {
                if (!_parentsPositions.ContainsKey(_activeWindow.Value)) return;

                var position = _parentsPositions[_activeWindow.Value];

                SetPlayerPosition(position);
            });
        }

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