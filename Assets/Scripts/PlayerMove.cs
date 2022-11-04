using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMove : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]private CharacterController _characterController;
    
    [Header("Player characteristics")]
    [SerializeField]private float _playerSpeed = 1f;
    [SerializeField] private float _leftRightOffset = 0f;
    [SerializeField] private float _upDownOffset = 0f;


    private OwnInput _input;
    private Vector2 _curMovementVector;
    private float _leftBoundX;
    private float _rightBoundX;
    private float _upBoundY;
    private float _downBoundY;

    private void Awake()
    {
        _input = new OwnInput();
        _input.Enable();
    }

    public void Start()
    {
        float defaultOffset = _characterController.radius;
        Camera camera = Camera.main;
        _leftBoundX = camera.ScreenToWorldPoint(Vector3.zero).x + defaultOffset + _leftRightOffset;
        _rightBoundX = camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - defaultOffset - _leftRightOffset;
        _upBoundY = camera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y - defaultOffset - _upDownOffset;
        _downBoundY = camera.ScreenToWorldPoint(Vector3.zero).y + defaultOffset + _upDownOffset; 

    }

    private void FixedUpdate()
    {
        _curMovementVector = _input.Player.Move.ReadValue<Vector2>();

        if (_curMovementVector != Vector2.zero) 
            OnMove(_curMovementVector);
    }


    public void OnMove(Vector2 movementVector)
    {
        Vector2 deltaPosition = movementVector * (_playerSpeed * Time.deltaTime);
        if (ValidateMovementVector(deltaPosition + GetCurrentPosition()))
        {
            _characterController.Move(deltaPosition);
        }
    }

    private bool ValidateMovementVector(Vector3 tempPosition)
    {
        if (tempPosition.x > _leftBoundX && tempPosition.x < _rightBoundX)
        {
            if (tempPosition.y > _downBoundY && tempPosition.y < _upBoundY)
            {
                return true;
            }
        }

        return false;
    }

    private Vector2 GetCurrentPosition() => 
        new Vector2(transform.position.x, transform.position.y);
}
