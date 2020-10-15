using System;
using UnityEngine;


namespace Data
{
    [Serializable]
    public class PetStruct
    {
        [Header("For Inspector")]
        public GameObject StoragePet;
        public Vector3 StartPosition;
        [HideInInspector] public GameObject Pet;
    }
}