using System;
using Unit.Player;
using UnityEngine;
using Object = UnityEngine.Object;

public class MyMoveRotateController : MonoBehaviour
{
    [SerializeField] private PlayerView _player;
    [SerializeField] private Transform _cameraTarget;
    [SerializeField] private Transform _enemyTarger;
    [SerializeField] private MyCameraView _camera;
    [SerializeField] private float _rotateSpeedPlayer = 90.0f;
    [SerializeField] private float _agroDistance = 5.0f;

    private Vector3 _direction = Vector3.zero;
    private GameObject _goTarget;


    private void Awake()
    {
        _goTarget = Object.Instantiate(new GameObject(), _player.Transform(), true);
        _goTarget.name = "";
    }

    private void Update()
    {
        _direction.x = UltimateJoystick.GetHorizontalAxis("MoveRotate");
        _direction.z = UltimateJoystick.GetVerticalAxis("MoveRotate");

        DebugExtension.DebugCircle(_player.Transform().position, Color.red, _agroDistance);
        
        MovePlayer(Time.deltaTime);
        RotatePlayer(Time.deltaTime);
        MoveCamera(Time.deltaTime);
        RotateCamera();
    }

    private void MovePlayer(float deltaTime)
    {
        _goTarget.transform.localPosition = _direction;
        var target = _player.Transform().position - _goTarget.transform.position;
        _player.Rigidbody().MovePosition(
            _player.Transform().position -
            target * (_player.Speed * deltaTime)
        );
    }

    private void RotatePlayer(float deltaTime)
    {
        var sqrDistance = (_player.Transform().position - _enemyTarger.position).sqrMagnitude;
        if (_agroDistance * _agroDistance > sqrDistance)
        {
            Debug.Log($"Противник рядом: {sqrDistance}");
            var newDir = Vector3.RotateTowards(
                _player.Transform().forward,
                (_enemyTarger.transform.position - _player.Transform().position),
                10f * Time.deltaTime, _agroDistance);
            newDir.y = 0;
            _player.Transform().rotation = Quaternion.LookRotation(newDir, Vector3.up);
        }
        else
        {
            Debug.Log($"Противника нет: {sqrDistance}");
            
            _player.Transform().RotateAround(
                _player.Transform().position,
                Vector3.up,
                _rotateSpeedPlayer * deltaTime * _direction.x);
        }
        
    }

    private void MoveCamera(float deltaTime)
    {
        _camera.transform.position = Vector3.Lerp(
            _camera.transform.position,
            _cameraTarget.transform.position,
            deltaTime * _camera._cameraSpeed);
        // _camera.transform.position = _player.Transform().localPosition - _camera._offSetPosition;
        // _camera.transform.LookAt(_player.Transform());
    }

    private void RotateCamera()
    {
        _camera.transform.LookAt(_player.Transform());
    }
}