using Interface;
using UnityEngine;


namespace Model.Player
{
    public sealed class FairyComponent: BaseObjectScene, IExecute
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _speedInterpolation;
        public void Execute()
        {
            Transform.position = Vector3.Lerp(Transform.position, _target.position + _offset, _speedInterpolation);
        }
    }
}