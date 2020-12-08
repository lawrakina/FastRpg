using Controller;
using Data;
using Extension;
using UnityEngine;


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
            return player.GetComponent<IPlayerView>();
        }

    }
}