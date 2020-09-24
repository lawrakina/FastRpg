﻿using UnityEngine;
using UnityEngine.AI;

namespace Model.Ai
{
    public static class Patrol
    {
        public static Vector3 GenericPoint(Transform agent)
        {
            // //todo перемещение по точкам
            // Vector3 result;
            //
            // //todo если плохая точка, то перебросить 
            // var dis = Random.Range(5, 50);
            // var randomPoint = Random.insideUnitSphere * dis;
            //
            // NavMesh.SamplePosition(agent.position + randomPoint, out var hit, dis, NavMesh.AllAreas);
            // result = hit.position;
            //
            // return result;
            
            //todo перемещение по точкам
            Vector3 result;
            
            //todo если плохая точка, то перебросить 
            var dis = Random.Range(0, 1);
            var randomPoint = Random.insideUnitSphere * dis;

            NavMesh.SamplePosition(agent.position + randomPoint, out var hit, dis,NavMesh.AllAreas);
            result = hit.position;

            return result;
        }
    }
}