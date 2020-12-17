using System;
using Enums;
using Interface;
using UniRx;
using Unit.Cameras;
using Unit.Player;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Controller
{
    public sealed class FightCameraController : BaseController, IExecute
    {
        #region Fields

        private IReactiveProperty<EnumBattleWindow> _battleState;
        private IReactiveProperty<EnumFightCamera> _typeCameraAndCharControl;
        private IPlayerView _player;
        private FightCamera _camera;
        private bool _followThePlayer;
        private float _deltaTime;

        private delegate void ActionMove(float deltaTime);

        private ActionMove _move;

        #endregion


        #region ClassLiveCycles

        public FightCameraController(IReactiveProperty<EnumBattleWindow> battleState, IPlayerView player,
            FightCamera camera, IReactiveProperty<EnumFightCamera> typeCameraAndCharControl)
        {
            _camera = camera;
            _player = player;
            _battleState = battleState;
            _typeCameraAndCharControl = typeCameraAndCharControl;

            _battleState.Subscribe(_ =>
            {
                if (_battleState.Value == EnumBattleWindow.Fight)
                {
                    _followThePlayer = true;
                }
                else
                {
                    _followThePlayer = false;
                }
            });
            _typeCameraAndCharControl.Subscribe(_ =>
            {
                if (_typeCameraAndCharControl.Value == EnumFightCamera.TopView)
                {
                    _move = TopViewFollow;
                }

                if (_typeCameraAndCharControl.Value == EnumFightCamera.ThirdPersonView)
                {
                    _move = ThirdPersonViewFollow;
                }
            });

            _camera.ThirdTarget = Object.Instantiate(
                new GameObject("ThirdPersonTargetCamera"),
                _player.Transform
            ).transform;
            _camera.ThirdTarget.localPosition = _camera.OffsetThirdPosition();
            _camera.TopTarget = _player.Transform;
        }

        #endregion


        #region IExecute

        public void Execute(float deltaTime)
        {
            _deltaTime = deltaTime;
            if (_followThePlayer)
            {
                _move(_deltaTime);
            }
        }

        #endregion


        #region PrivateMethods

        private void ThirdPersonViewFollow(float deltaTime)
        {
            _camera.ThirdTarget.localPosition = _camera.OffsetThirdPosition();
            //CameraMove
            // _camera.transform.position = Vector3.Lerp(
            //     _camera.transform.position,
            //     _camera.ThirdTarget.position,
            //     deltaTime * _camera.CameraMoveSpeed);
            _camera.transform.position = _camera.ThirdTarget.position;
            //CameraRotate
            _camera.transform.LookAt(_player.Transform);
        }

        private void TopViewFollow(float deltaTime)
        {
            _camera.transform.position = Vector3.Lerp(
                _camera.transform.position,
                _player.Transform.position + _camera.OffsetTopPosition(),
                deltaTime * _camera.CameraMoveSpeed);
            _camera.transform.rotation = Quaternion.Euler(_camera.OffsetTopRotation());
        }

        #endregion
    }
}