using Controller;
using Manager;
using UnityEngine;
using VIew;


namespace Initializator
{
    public sealed class PetInitializator
    {
        public PetInitializator(Services services, GameContext gameContext)
        {
            var spawnPet = Object.Instantiate(gameContext.PetData.PetStruct.StoragePet,
                gameContext.PetData.PetStruct.StartPosition,
                Quaternion.identity);

            var petView = spawnPet.GetComponent<PetView>();
            gameContext.PetData.PetStruct.Pet = petView.gameObject;

            var zonesActivate = GameObject.FindGameObjectsWithTag(TagManager.TAG_PET_SPAWN_ZONE);

            foreach (var zone in zonesActivate)
            {
                zone.GetComponent<ZoneActivatedView>().GameObjectForEnable = spawnPet;
            }
                
            //     .Find($"FairyZone").GetComponent<ZoneActivatedView>();
            // zoneActivate.GameObjectForEnable = spawnPet;
            
            services.PetController = new PetController(services, gameContext, petView);
            services.MainController.AddUpdated(services.PetController);
        }
    }
}