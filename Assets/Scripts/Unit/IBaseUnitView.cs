using UnityEngine;

namespace Unit
{
    public interface IBaseUnitView
    {
        Transform          Transform();
        Collider           Collider();
        Rigidbody          Rigidbody();
        MeshRenderer       MeshRenderer();
        Animator           Animator();
        AnimatorParameters AnimatorParameters();
        float              Speed             { get; set; }
        Transform          EnemyTarget       { get; set; }
        float              AgroDistance      { get; }
        float              RotateSpeedPlayer { get; }
    }
}