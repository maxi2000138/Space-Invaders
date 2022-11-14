using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMove : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private CharacterController _characterController;
        
        [Header("Player characteristics")]
        [SerializeField] private float _playerSpeed = 1f;
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
            _input.Player.Enable();
            _input.Player.Move.performed += OnMove2;
        }

        public void Start()
        {
            float defaultOffset = _characterController.radius;
            _leftBoundX = PlayingFieldBorders.LeftBoundX + _leftRightOffset + defaultOffset;
            _rightBoundX = PlayingFieldBorders.RightBoundX - defaultOffset - _leftRightOffset;
            _upBoundY = PlayingFieldBorders.UpBoundY - defaultOffset - _upDownOffset;
            _downBoundY = PlayingFieldBorders.DownBoundY + defaultOffset + _upDownOffset;

        }
 
        private void FixedUpdate()
        {
           // _curMovementVector = _input.Player.Move.ReadValue<Vector2>();

            //if (_curMovementVector != Vector2.zero) 
             //    OnMove(_curMovementVector);
        }


        public void OnMove(Vector2 movementVector)
        {
            Vector2 deltaPosition = movementVector * (_playerSpeed * Time.deltaTime);
            if (ValidateMovementVector(deltaPosition + GetCurrentPosition()))
            {
                _characterController.Move(deltaPosition);
            }
        }

        public void OnMove2(InputAction.CallbackContext position)
        {

            Vector3 deltaPos = position.ReadValue<Vector2>();
            deltaPos = Camera.main.ScreenToWorldPoint(deltaPos);
            deltaPos.z = transform.position.z;
            transform.position = deltaPos;
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

        private Vector2 GetCurrentPosition()
        {
            Vector3 currentPosition = transform.position;
            return new Vector2(currentPosition.x, currentPosition.y);
        }
    }
    
}
