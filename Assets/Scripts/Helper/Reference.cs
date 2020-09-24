using UnityEngine;
using Model.Ai;
// using UnityEditor.Experimental.GraphView;


namespace Helper
{
    public sealed class Reference : MonoBehaviour
    {
        public Bot Bot;
        [SerializeField] private int _botsCount = 10;
        public Enemy EnemyMele;
        public Enemy EnemyMage;
        public Enemy EnemyRoque;
        public Enemy EnemyRange;

        public int BotsCount()
        {
            return _botsCount;
        }
    }
}