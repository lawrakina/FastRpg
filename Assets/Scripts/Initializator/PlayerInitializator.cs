using Controller;
using Model;
using UnityEngine;
using VIew;


namespace Initializator
{
    public sealed class PlayerInitializator
    {
        public PlayerInitializator(Services services, GameContext gameContext)
        {
            var spawnerPlayer = Object.Instantiate(gameContext.PlayerData.PlayerStruct.StoragePlayer,
                gameContext.PlayerData.PlayerStruct.StartPosition,
                Quaternion.identity);

            var playerModel = new BaseUnitModel()
            {
                Hp = gameContext.PlayerData.PlayerStruct.Hp,
                MaxHp = gameContext.PlayerData.PlayerStruct.Hp,
            };

            var playerView = spawnerPlayer.GetComponent<PlayerView>();
            gameContext.PlayerData.PlayerStruct.Player = playerView.gameObject;

            var healthBarView = spawnerPlayer.GetComponentInChildren<HealthBarView>();

            services.PlayerController =
                new PlayerController(services, gameContext, playerModel, playerView, healthBarView);
            
            services.MainController.AddUpdated(services.PlayerController);
            services.MainController.AddFixedUpdated(services.PlayerController);
            services.MainController.AddEnabledAndDisabled(services.PlayerController);
            services.MainController.AddLateUpdated(services.PlayerController);
        }
    }
}