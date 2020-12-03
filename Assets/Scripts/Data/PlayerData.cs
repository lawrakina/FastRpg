using UnityEngine;

namespace Controller
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] public GameObject StoragePlayer;
    }
}