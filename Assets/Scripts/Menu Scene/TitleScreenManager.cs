using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

namespace UU
{
    public class TitleScreenManager : MonoBehaviour
    {
        [SerializeField] private Button _pressStartButton;
        [SerializeField] private Button _startNewGameButton;

        private Coroutine _startNewGame;
        
        private void Start()
        {
            if (_pressStartButton != null)
            {
                _pressStartButton.onClick.AddListener(OnPressStartButtonHandleClick);
                _pressStartButton.Select();
            }
            
            if (_startNewGameButton != null)
            {
                _startNewGameButton.onClick.AddListener(OnStartNewGameButtonHandleClick);
            }
        }
        
        private void OnDisable()
        {
            _pressStartButton.onClick.RemoveListener(OnPressStartButtonHandleClick);
        }

        //  NETCODE
        public void StartNetworkAsHost()
        {
            NetworkManager.Singleton.StartHost();
        }
        
        //  SCENE MANAGEMENT
        public void StartWork()
        {
            StopWork();

            _startNewGame = StartCoroutine(WorldSaveGameManager.instance.LoadNewGame());
        }

        public void StopWork()
        {
            if (_startNewGame != null)
                StopCoroutine(_startNewGame);
        }

        //  UI
        private void OnPressStartButtonHandleClick()
        {
            Debug.Log("HOST HERE");
            StartNetworkAsHost();
            
            _pressStartButton.gameObject.SetActive(false);
            _startNewGameButton.gameObject.SetActive(true);
            _startNewGameButton.Select();
        }
        
        private void OnStartNewGameButtonHandleClick()
        {
            Debug.Log("START NEW GAME");
            StartWork();
        }
    }
}