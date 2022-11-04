using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]private float _playerSpeed = 1f;
    [SerializeField]private CharacterController _characterController;
    private float _leftBoundX;
    private float _rightBoundX;
    private float _upBoundY;
    private float _downBoundY;

    public void Start()
    {
        float offset = _characterController.radius;
        _leftBoundX = Camera.main.ScreenToWorldPoint(Vector3.zero).x + offset;
        _rightBoundX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - offset;
        _upBoundY = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y - offset;
        _downBoundY = Camera.main.ScreenToWorldPoint(Vector3.zero).y + offset; 

    }
    
 
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 tempPosition = (context.ReadValue<Vector2>() * _playerSpeed);
            if (ValidateMovementVector(tempPosition + GetCurrentPosition()))
            {
                _characterController.Move(tempPosition);
            }
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
