﻿using System;
using Extension;
using Interface;
using UnityEngine;
using UnityEngine.Rendering;


namespace Unit.Cameras
{
    public sealed class FightCamera : MonoBehaviour, IFightCamera
    {
        #region Fields

        [Header("Top down camera orientation")]
        [SerializeField]
        private readonly Vector3 _offetTopPosition = new Vector3(-15.0f, 30.0f, 15.0f);

        [SerializeField]
        private readonly Vector3 _offsetTopRotation = new Vector3(45.0f, 135.0f, 0.0f);

        [Header("Third person camera orientation")]
        [SerializeField]
        private Vector3 _offsetThirdPosition = new Vector3(0.0f, 10.0f, -24.0f);

        [SerializeField]
        private Vector3 _offsetThirdRotation = new Vector3(0.0f, 0.0f, 0.0f);

        [SerializeField]
        private readonly float _cameraMoveSpeed = 3.0f;

        [SerializeField]
        private readonly float _cameraRotateSpeed = 90.0f;

        [Header("For Debug, pls do not set:")]
        [SerializeField]
        private Transform _topTarget;

        [SerializeField]
        private Transform _thidrTarget;

        #endregion


        #region Properties

        public Vector3 OffsetTopPosition()
        {
            return _offetTopPosition;
        }

        public Vector3 OffsetTopRotation()
        {
            return _offsetTopRotation;
        }

        public Vector3 OffsetThirdPosition()
        {
            return _offsetThirdPosition;
        }

        public Vector3 OffsetThirdRotation()
        {
            return _offsetThirdRotation;
        }

        public float CameraMoveSpeed => _cameraMoveSpeed;
        public float CameraRotateSpeed => _cameraRotateSpeed;

        public Transform TopTarget
        {
            get => _topTarget;
            set => _topTarget = value;
        }

        public Transform ThirdTarget
        {
            get => _thidrTarget;
            set => _thidrTarget = value;
        }

        #endregion


        private void OnCollisionEnter(Collision other)
        {
            Dbg.Log($"OnTriggerEnter.other:{other},{other.gameObject},isStatic:{other.gameObject.isStatic}");
            if (other.gameObject.isStatic)
            {
                var go = other.gameObject.GetComponent<MeshRenderer>();
                go.shadowCastingMode = ShadowCastingMode.ShadowsOnly;
            }
        }

        // OnCollisionEnter OnCollisionExit OnTriggerEnter  OnTriggerExit

        private void OnCollisionExit(Collision other)
        {
            Dbg.Log($"OnTriggerExit.other:{other},{other.gameObject},isStatic:{other.gameObject.isStatic}");
            if (other.gameObject.isStatic)
            {
                var go = other.gameObject.GetComponent<MeshRenderer>();
                go.shadowCastingMode = ShadowCastingMode.On;
            }
        }
    }
}