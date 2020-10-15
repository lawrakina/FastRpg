using UnityEngine;


namespace Data
{
    [CreateAssetMenu(fileName = "PetData", menuName = "Units/PetData")]
    public class PetData : ScriptableObject
    {
        public PetStruct PetStruct;
    }
}