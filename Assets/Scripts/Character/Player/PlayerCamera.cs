using UnityEngine;
using UnityEngine.Serialization;

namespace UU
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] public PlayerManager Player;
        [SerializeField] private float _leftAndRightLookAngle;
        [SerializeField] private float _upAndDownLookAngle;
        [SerializeField] private float _leftAndRightRotationSpeed = 220;
        [SerializeField] private float _upAndDownRotationSpeed = 220;
        [SerializeField] private float _minimumPivot = -30;  //  Определяет нижнюю границу поворота камеры.
        [SerializeField] private float _maximumPivot = 60;   //  Определяет верхнюю границу поворота камеры.
        [SerializeField] private Transform _cameraPivotTransform;
        [SerializeField] private float _cameraCollisionRadius = 0.2f;
        [SerializeField] private LayerMask _collideWithLayers;
        
        public static PlayerCamera Instance;
        public Camera CameraObject;
        
        private float _cameraSmoothSpeed = 1;
        private Vector3 _cameraVelocity;
        private Vector3 _cameraObjectPosition;
        private float _cameraZPosition;
        private float _targetCameraZPosition;
        private float _cameraObjectPositionZInterpolation = 0.2f;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            _cameraZPosition = CameraObject.transform.localPosition.z;
        }
        
        public void HandleAllCameraActions()
        {
            if (Player != null)
            {
                HandleFollowTarget();
                HandleRotation();
                HandleCollisions();
            }
        }

        private void HandleFollowTarget()
        {
            Vector3 targetCameraPosition = Vector3.SmoothDamp(
                transform.position,
                Player.transform.position,
                ref _cameraVelocity,
                _cameraSmoothSpeed * Time.deltaTime);

            transform.position = targetCameraPosition;
        }

        private void HandleRotation()
        {
            _leftAndRightLookAngle += (PlayerInputManager.Instance._cameraHorizontalInput * _leftAndRightRotationSpeed) * Time.deltaTime;
            _upAndDownLookAngle -= (PlayerInputManager.Instance._cameraVerticalInput * _upAndDownRotationSpeed) * Time.deltaTime;
            _upAndDownLookAngle = Mathf.Clamp(_upAndDownLookAngle, _minimumPivot, _maximumPivot);

            Vector3 cameraRotation = Vector3.zero;
            Quaternion targetRotation;
            
            cameraRotation.y = _leftAndRightLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            transform.rotation = targetRotation;

            cameraRotation = Vector3.zero;
            cameraRotation.x = _upAndDownLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            _cameraPivotTransform.localRotation = targetRotation;
        }

        private void HandleCollisions()
        {
            _targetCameraZPosition = _cameraZPosition;
            RaycastHit hit;
            Vector3 direction = CameraObject.transform.position - _cameraPivotTransform.position;
            direction.Normalize();

            if (Physics.SphereCast(
                    _cameraPivotTransform.position, 
                    _cameraCollisionRadius, 
                    direction, 
                    out hit,
                    Mathf.Abs(_targetCameraZPosition), 
                    _collideWithLayers))
            {
                float distanceFromHitObject = Vector3.Distance(_cameraPivotTransform.position, hit.point);
                _targetCameraZPosition = -(distanceFromHitObject - _cameraCollisionRadius);
            }

            if (Mathf.Abs(_targetCameraZPosition) < _cameraCollisionRadius)
            {
                _targetCameraZPosition = -_cameraCollisionRadius;
            }
            
            _cameraObjectPosition.z = Mathf.Lerp(CameraObject.transform.localPosition.z, _targetCameraZPosition, _cameraObjectPositionZInterpolation);
            CameraObject.transform.localPosition = _cameraObjectPosition;
        }
    }
}