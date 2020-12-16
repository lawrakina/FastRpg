using System;
using Controller;
using Data;
using Enums;
using Extension;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Unit.Player
{
    public sealed class PlayerFactory : IPlayerFactory
    {
        #region fields

        private readonly PlayerData _playerData;

        #endregion


        #region ctor

        public PlayerFactory(PlayerData playerData)
        {
            _playerData = playerData;
        }

        #endregion


        public IPlayerView CreatePlayer()
        {
            var player = Object.Instantiate(_playerData.StoragePlayer);
            player.name = $"PlayerCharacter";
            player.AddCapsuleCollider(radius: 0.5f, isTrigger: false, 
                      center: new Vector3(0.0f,0.9f,0.0f),
                      height: 1.8f)
                  .AddRigidBody(mass: 80, CollisionDetectionMode.ContinuousSpeculative, 
                      isKinematic: false, useGravity: true, 
                      constraints: RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezeRotationZ)
                  .AddCode<PlayerView>();
            
            var component = player.GetComponent<PlayerView>();
            component.Speed = _playerData.PlayerMoveSpeed;
            component.AgroDistance = _playerData.AgroDistance;
            component.RotateSpeedPlayer = _playerData.RotateSpeedPlayer;
            component.PlayerSettings = _playerData.PlayerSettings;
            switch (_playerData.PlayerSettings.CharacterClass)
            {
                case CharacterClass.Warrior:
                    component.CharacterClass = new CharacterClassWarrior();
                    break;
                case CharacterClass.Rogue:
                    component.CharacterClass = new CharacterClassRogue();
                    break;
                case CharacterClass.Hunter:
                    component.CharacterClass = new CharacterClassHunter();
                    break;
                case CharacterClass.Mage:
                    component.CharacterClass = new CharacterClassMage();
                    break;
                default:
                    component.CharacterClass = null;
                    break;
            }
            return player.GetComponent<IPlayerView>();
        }

    }
}