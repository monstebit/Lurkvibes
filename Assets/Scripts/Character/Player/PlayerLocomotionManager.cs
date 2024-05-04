using UnityEngine;

namespace UU
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        [SerializeField] private PlayerManager _player;
        [SerializeField] private float _walkingSpeed = 2;
        [SerializeField] private float _runningSpeed = 5;
        [SerializeField] private float _rotationSpped = 15;
        
        private float _verticalMovement;
        private float _horizontalMovement;
        private float _moveAmount;
        private Vector3 _moveDirection;
        private Vector3 _targetRotationDirection;
        
        private new void Awake()
        {
            base.Awake();
        }
        
        //  TO DO: public ЗДЕСЬ МЕНЯ СМУЩАЕТ => РЕАЛИЗОВАТЬ ПАТТЕРН MVP
        public void HandleAllMovement()
        {
            HandleGroundedMovement();
            HandleRotation();
        }

        private void GetVerticalAndHorizontalInputs()
        {
            _verticalMovement = PlayerInputManager.Instance._verticalInput;
            _horizontalMovement = PlayerInputManager.Instance._horizontalInput;
        }

        private void HandleGroundedMovement()
        {
            GetVerticalAndHorizontalInputs();
            
            _moveDirection = PlayerCamera.Instance.transform.forward * _verticalMovement;
            _moveDirection = _moveDirection + PlayerCamera.Instance.transform.right * _horizontalMovement;
            _moveDirection.Normalize();
            _moveDirection.y = 0;

            if (PlayerInputManager.Instance._moveAmount > 0.5f)
            {
                _player.CharacterController.Move(_moveDirection * _runningSpeed * Time.deltaTime);
            }
            else if (PlayerInputManager.Instance._moveAmount >= 0.5f)
            {
                _player.CharacterController.Move(_moveDirection * _walkingSpeed * Time.deltaTime);
            }
        }

        private void HandleRotation()
        {
            _targetRotationDirection = Vector3.zero;
            _targetRotationDirection = PlayerCamera.Instance.CameraObject.transform.forward * _verticalMovement;
            _targetRotationDirection = _targetRotationDirection + PlayerCamera.Instance.CameraObject.transform.right * _horizontalMovement;
            _targetRotationDirection.Normalize();
            _targetRotationDirection.y = 0;

            if (_targetRotationDirection == Vector3.zero)
            {
                _targetRotationDirection = transform.forward;
            }

            Quaternion newRotation = Quaternion.LookRotation(_targetRotationDirection);
            Quaternion targetRotation = Quaternion.Slerp(
                transform.rotation, 
                newRotation, 
                _rotationSpped * Time.deltaTime);

            transform.rotation = targetRotation;
        }
    }
}