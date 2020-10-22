using Data;
using UnityEngine;


namespace Controller
{
    public class GameContext
    {
        public PlayerData PlayerData;
        public LayerMask ObjectsToHideLayer;
        public LayerMask GroundLayer;
        public LayerMask LayerUnits;
        public LayerMask WaterLayer;
        public PetData PetData;
        public GameObject WaterZone;
    }
}