using System;
using Manager;
using UnityEngine;


namespace VIew
{
    public class ZoneActivatedView : MonoBehaviour
    {
        #region Fields
        
        public GameObject GameObjectForEnable;

        #endregion


        private void OnTriggerEnter(Collider other)
        {
            if (!other) return;
            if (other.CompareTag(TagManager.PLAYER))
            {
                GameObjectForEnable.GetComponent<BaseUnitView>().isEnable = true;
            }
        }
    }
}
