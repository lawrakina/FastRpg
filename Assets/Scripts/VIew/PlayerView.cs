using Data;
using UnityEngine;
using UnityEngine.AI;


namespace VIew
{
    public sealed class PlayerView: BaseUnitView
    {
        
        

        public PlayerView(PlayerStruct playerStruct)
        {
            
        }

        public void EnterTheZone()
        {
            Debug.Log($"Enter the zone");
        }
    }
}