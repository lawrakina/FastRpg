using UnityEngine;


namespace Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Units/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        public PlayerStruct PlayerStruct;
    }
}