using Data;
using UnityEngine;


namespace Controller
{
    public class GameContext
    {
        public PlayerData PlayerData;
        public Camera Camera { get; set; }
        public LayerMask GroundLayer;
        public PetData PetData;
    }
}