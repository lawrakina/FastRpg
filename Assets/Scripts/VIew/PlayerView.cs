using Data;
using UnityEngine;
using UnityEngine.AI;


namespace VIew
{
    public sealed class PlayerView: BaseUnitView
    {
        #region Properties

        public float distanceToCheckGround = 0.51f;
        public float accelerationOfGravity = 1f;

        #endregion
        
        

        public PlayerView(PlayerStruct playerStruct)
        {
            
        }

        public void EnterTheZone()
        {
            Debug.Log($"Enter the zone");
        }
    }
}