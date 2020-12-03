using UnityEngine;

namespace Unit
{
    public interface IBaseUnitView
    {
        Transform    Transform();
        Collider     Collider();
        Rigidbody    Rigidbody();
        MeshRenderer MeshRenderer();
        Animator     Animator();
        AnimatorParameters AnimatorParameters(Animator animator);
    }
}