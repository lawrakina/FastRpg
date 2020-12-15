using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] public GameObject StoragePlayer;
        [SerializeField] public float PlayerMoveSpeed = 10.0f;
        [SerializeField] public float AgroDistance = 10.0f;
        [SerializeField] public float RotateSpeedPlayer = 90.0f;
    }
}