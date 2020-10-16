using Controller;
using UnityEngine;
using VIew;

namespace Initializator
{
    public sealed class PlayerInitializator
    {
        public PlayerInitializator(Services services, GameContext gameContext)
        {
            var spawnerPlayer = Object.Instantiate( gameContext.PlayerData.PlayerStruct.StoragePlayer,
                gameContext.PlayerData.PlayerStruct.StartPosition,
                Quaternion.identity);
            
            // PlayerStruct playerStruct = gameContext.PlayerData.PlayerStruct;
            // playerStruct.Player = spawnerPlayer;

            var playerView = spawnerPlayer.GetComponent<PlayerView>();
            gameContext.PlayerData.PlayerStruct.Player = playerView.gameObject;
            // var playerComponent = new PlayerView(playerStruct);
            // var playerView = new PlayerView(playerStruct);
            
            services.PlayerController = new PlayerController(services, gameContext, playerView);
            services.MainController.AddUpdated(services.PlayerController);
            services.MainController.AddFixedUpdated(services.PlayerController);
        }
    }
}