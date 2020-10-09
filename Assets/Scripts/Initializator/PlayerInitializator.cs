using Controller;
using Manager;
using UnityEngine;

namespace Initializator
{
    public sealed class PlayerInitializator
    {
        public PlayerInitializator(Services services, GameContext gameContext)
        {
            var spawnerPlayer = Object.Instantiate( gameContext.PlayerData.PlayerStruct.StoragePlayer,
                gameContext.PlayerData.PlayerStruct.StartPosition.position,
                gameContext.PlayerData.PlayerStruct.StartPosition.rotation);
            
            PlayerStruct playerStruct = gameContext.PlayerData.PlayerStruct;
            playerStruct.Player = spawnerPlayer;

            var playerModel = new PlayerComponent(playerStruct);
            var playerView = new PlayerView(playerStruct);
            
            // services.PlayerController = new PlayerController(services, gameContext, playerModel, playerView);
            services.MainController.AddUpdated(services.PlayerController);
        }
    }
}