using UnityEngine;

namespace Controller
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData")]
    public class PlayerData : ScriptableObject
    {
        public PlayerStruct PlayerStruct;
    }
}