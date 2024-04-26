using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UU
{
    public class PlayerInputManager : MonoBehaviour
    {
        [SerializeField] private Vector2 _movementInput;
        [SerializeField] private float _verticalInput;
        [SerializeField] private float _horizontalInput;
        [SerializeField] private float _moveAmount;
        
        private PlayerControls _playerControls;
        public static PlayerInputManager Instance;

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
            
            //  ЕСЛИ СЦЕНА ПОМЕНЯЛАСЬ, ВЫПОЛНИ ЭТУ ЛОГИКУ
            SceneManager.activeSceneChanged += OnSceneChange;
            
            Instance.enabled = false;
        }

        private void OnEnable()
        {
            if (_playerControls == null)
            {
                _playerControls = new PlayerControls();

                _playerControls.PlayerMovement.Movement.performed += i => _movementInput = i.ReadValue<Vector2>();
            }
            
            _playerControls.Enable();
        }

        private void Update()
        {
            HandleMovementInput();
        }

        //  ИЛИ ОТПИСЫВАТЬСЯ ЛУЧШЕ В DISABLE?
        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= OnSceneChange;
        }

        private void OnSceneChange(Scene oldScene, Scene newScene)
        {
            if (newScene.buildIndex == WorldSaveGameManager.Instance.WorldSceneIndex)
            {
                Instance.enabled = true;
            }
            //  ОТКЛЮЧАЕМ PLAYERS CONTROLS В ГЛАВНОМ МЕНЮ
            else
            {
                Instance.enabled = false;
            }
        }

        private void HandleMovementInput()
        {
            _verticalInput = _movementInput.y;
            _horizontalInput = _movementInput.x;

            //  ВОВЗРАЩАЕТ АБСОЛЮТНОЕ ЧИСЛО (ЭТО ЗНАЧИТ БЕЗ ЗНАКА МИНУС)
            _moveAmount = Mathf.Clamp01(Mathf.Abs(_verticalInput) + Mathf.Abs(_horizontalInput));

            if (_moveAmount <= 0.5 && _moveAmount > 0)
            {
                _moveAmount = 0.5f;
            }
            else if (_moveAmount > 0.5 && _moveAmount <= 1)
            {
                _moveAmount = 1;
            }
        }
    }
}