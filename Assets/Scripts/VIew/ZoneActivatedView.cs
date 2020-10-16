using System;
using Manager;
using UnityEngine;


namespace VIew
{
    public class ZoneActivatedView : MonoBehaviour
    {
        #region Fields
        
        public GameObject GameObjectForEnable;
        private BaseUnitView _baseUnit;

        #endregion


        private void OnTriggerEnter(Collider other)
        {
            if (!other) return;
            Debug.Log($"ZoneActivatedView.OnTriggerEnter.other: {other.name}");
            if (!other.CompareTag(TagManager.TAG_PLAYER)) return;
            other.SendMessage($"EnterTheZone");
            _baseUnit = GameObjectForEnable.GetComponent<BaseUnitView>();
            _baseUnit.isEnable = !_baseUnit.isEnable;
        }
    }
}
