using UnityEngine;


public class RagdollController : MonoBehaviour
{
    [SerializeField ]private Rigidbody _rigidbodyToPush;
    [SerializeField] private float _killForce = 5.0f;
    [SerializeField] private bool _kill;
    [SerializeField] private bool _revive;

    private Rigidbody[] _rigidbodies;
    private Collider[] _colliders;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _colliders = GetComponentsInChildren<Collider>();

        Revive();      
    }

    private void Update()
    {
        if(_kill == true)
        {
            Kill(Vector3.up);
        }

        if (_revive == true)
        {
            Revive();
        }
    }

    public void Kill(Vector3 infoDirection)
    {
        _kill = false;        

        SetRagdollState(true);
        SetMainPhysics(false);

        var body = _rigidbodyToPush;//_rigidbodies[6];
        var force = infoDirection;

        force = force * _killForce * body.mass;
        body.AddForce(force, ForceMode.Impulse);
    }

    private void Revive()
    {
        _revive = false;

        SetRagdollState(false);
        SetMainPhysics(true);
    }

    private void SetRagdollState(bool activityState)
    {
        for (int i = 1; i < _rigidbodies.Length; i++)
        {
            _rigidbodies[i].isKinematic = !activityState;
            _colliders[i].enabled = activityState;
        }

        //for (int i = 1; i < _colliders.Length; i++)
        //{
            
        //}
    }

    private void SetMainPhysics(bool activityState)
    {
        _animator.enabled = activityState;
        _rigidbodies[0].isKinematic = !activityState;
        // _colliders[0].enabled = activityState;
    }
}
