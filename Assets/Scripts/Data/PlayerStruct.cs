using System;
using UnityEngine;


namespace Controller
{
    [Serializable]
    public class PlayerStruct
    {
        [Header("For Inspector")]
        public GameObject StoragePlayer;
        public Transform StartPosition;
        public float Hp = 100.0f;
        public float Speed = 5.0f;
        public float TimeDisableBattleState = 5.0f;
        public float UpdatePeriodVisibility = 1.0f;
        public float _battleDistance = 15.0f;
        public LayerMask EnemiesLayerMask;
        [HideInInspector] public GameObject Player;
    }
}