using System;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class IKController : MonoBehaviour
{
    #region Fields

    [SerializeField] private Transform _rightHandObject = null;
    [SerializeField] private Transform _leftHandObject = null;
    [SerializeField] private Transform _lookObject = null;

    [SerializeField] private bool _ikActive = false;
    [SerializeField] private float _activeDistance = 2.0f;

    #endregion

    
    #region Properties

    protected Animator _animator;

    #endregion

    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _ikActive = Vector3.Distance(transform.position, _lookObject.position) < _activeDistance;
    }

    private void OnAnimatorIK()
    {
        if (_ikActive)
        {
            if (_lookObject != null)
            {
                _animator.SetLookAtWeight(1);
                _animator.SetLookAtPosition(_lookObject.position);
            }

            if (_rightHandObject != null)
            {
                _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                _animator.SetIKPosition(AvatarIKGoal.RightHand, _rightHandObject.position);
                _animator.SetIKRotation(AvatarIKGoal.RightHand, _rightHandObject.rotation);
            }

            if (_leftHandObject != null)
            {
                _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                _animator.SetIKPosition(AvatarIKGoal.LeftHand, _leftHandObject.position);
                _animator.SetIKRotation(AvatarIKGoal.LeftHand, _leftHandObject.rotation);
            }
        }
        else
        {
            _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            _animator.SetLookAtWeight(0);
        }
    }
}