using UnityEngine;
using UnityEngine.SceneManagement;

namespace UU
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager Instance;
        
        [SerializeField] private Vector2 _movementInput;
        
        private PlayerControls _playerControls;

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
    }
}