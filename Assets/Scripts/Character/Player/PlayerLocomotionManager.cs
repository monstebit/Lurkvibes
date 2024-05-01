using UnityEngine;

namespace UU
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        [SerializeField] private PlayerManager _player;
        [SerializeField] private float _walkingSpeed = 2;
        [SerializeField] private float _runningSpeed = 5;
        
        private float _verticalMovement;
        private float _horizontalMovement;
        private float _moveAmount;
        private Vector3 _moveDirection;
        
        private new void Awake()
        {
            base.Awake();
        }
        
        //  TO DO: public ЗДЕСЬ МЕНЯ СМУЩАЕТ => РЕАЛИЗОВАТЬ ПАТТЕРН MVP
        public void HandleAllMovement()
        {
            HandleGroundedMovement();
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
    }
}