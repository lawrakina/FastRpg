using Components;
using Controller;
using UnityEngine;
using UnityEngine.AI;


namespace Initializator
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(NavMeshAgent))]
    
    
    
    public sealed class PlayerView: BaseUnitView
    {
        public PlayerView(PlayerStruct playerStruct)
        {
            
        }
    }
}