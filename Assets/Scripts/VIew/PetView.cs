using UnityEngine;


namespace VIew
{
    public sealed class PetView : BaseUnitView
    {
        #region Fields

        public Vector3 OffsetPosition = new Vector3(2.5f, 1.0f,0.0f);
        public GameObject Target;
        public float Intencity = 1.0f;
        public Light Light;

        #endregion

        protected override void Awake()
        {
            base.Awake();
            Light = GetComponentInChildren<Light>();
            isEnable = false;
        }
    }
}