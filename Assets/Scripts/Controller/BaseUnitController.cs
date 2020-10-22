using System;
using Enums;
using Interface;
using Model;
using UnityEngine;
using VIew;


namespace Controller
{
    public abstract class BaseUnitController : IUpdated, IFixedUpdate, IEnabled, ILateUpdated
    {
        #region Fields

        protected Services _services;
        protected GameContext _context;
        protected BaseUnitView _baseUnitView;
        protected BaseUnitModel _model;
        protected HealthBarView _healthBarView;

        #endregion


        #region tempFieds

        protected Vector3 Direction;
        protected float GravityForce;

        #endregion


        #region ctor

        protected BaseUnitController(
            Services services, GameContext context,
            BaseUnitModel baseUnitModel,
            BaseUnitView baseUnitView,
            HealthBarView healthBarView)
        {
            _services = services;
            _context = context;
            _baseUnitView = baseUnitView;
            _model = baseUnitModel;
            _healthBarView = healthBarView;
        }

        #endregion


        #region IEnabled

        public virtual void On()
        {
            _healthBarView.On();

            if (_baseUnitView == null) return;
            _baseUnitView.OnSwimEvent += ToSwim;
            _baseUnitView.OnUnSwimEvent += ToUnSwim;
        }

        private void ToUnSwim()
        {
            // _baseUnitView.Rigidbody.useGravity = true;
        }

        private void ToSwim()
        {
            Debug.Log($"ToSwim");
            // _baseUnitView.Rigidbody.useGravity = false;
        }

        public virtual void Off()
        {
            _healthBarView.Off();
        }

        #endregion


        #region IUdpatable,ILateUpdate,IFixedUpdate

        public virtual void LateUpdateTick()
        {
            HealthAlignCamera();
        }

        public virtual void UpdateTick()
        {
            switch (_model.UnitState)
            {
                case UnitState.None:
                    return;
                    break;
                case UnitState.Dead:
                    return;
                    break;
                case UnitState.Normal:
                    break;
                case UnitState.Swim:
                    if (_baseUnitView.AnimatorParams.UnitState == UnitState.Swim) break;
                    _baseUnitView.AnimatorParams.UnitState = UnitState.Swim;
                    break;
                case UnitState.Fly:
                    if (_baseUnitView.AnimatorParams.UnitState == UnitState.Fly) break;
                    _baseUnitView.AnimatorParams.UnitState = UnitState.Fly;
                    break;
                case UnitState.Stunned:
                    if (_baseUnitView.AnimatorParams.UnitState == UnitState.Stunned) break;
                    _baseUnitView.AnimatorParams.UnitState = UnitState.Stunned;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            CheckGravity();
            UpdateHealthBar();
        }

        public virtual void FixedUpdateTick()
        {
            Move();
        }

        #endregion


        #region PublicMethods

        public abstract void Move(Vector3 moveVector);

        #endregion


        #region PrivateMethods

        private void CheckGravity()
        {
            if (IsGrounded)
            {
                _model.UnitState = UnitState.Normal;
                GravityForce = -1.0f;
            }
            else if (IsInWater)
            {
                _model.UnitState = UnitState.Swim;
                GravityForce = -1.0f;
            }
            else
            {
                _model.UnitState = UnitState.Fly;
                GravityForce -= 2.0f;
            }
            
            Direction.y = GravityForce;
        }

        private bool IsInWater =>
            Physics.Raycast(_baseUnitView.transform.position + Vector3.up / 2, Vector3.down, out _,
                _baseUnitView.distanceToCheckGround, _context.WaterLayer);

        protected abstract void Move();

        private bool IsGrounded =>
            // RaycastHit hit;
            // Debug.DrawRay(_player.transform.position + new Vector3(0.0f, 0.5f, 0.0f), Vector3.down, Color.red,
            // _player.distanceToCheckGround);
            Physics.Raycast(_baseUnitView.transform.position + Vector3.up / 2, Vector3.down, out _,
                _baseUnitView.distanceToCheckGround, _context.GroundLayer);

        private void UpdateHealthBar()
        {
            if (_model.Hp <= _model.MaxHp)
            {
                _healthBarView.MeshRenderer.enabled = true;
                _healthBarView.UpdateParams(_model.Hp, _model.MaxHp);
            }
            else
            {
                _healthBarView.MeshRenderer.enabled = false;
            }
        }

        private void HealthAlignCamera()
        {
            if (_model.Hp <= _model.MaxHp)
            {
                _healthBarView.AlignCamera(_services.ThirdCameraController.CameraTransform);
            }
        }

        #endregion
    }
}