using System;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UU
{
    public class TitleScreenManager : MonoBehaviour
    {
        [SerializeField] private Button _pressStartButton;
        [SerializeField] private Button _startNewGameButton;
        
        private void Start()
        {
            if (_pressStartButton != null)
            {
                _pressStartButton.onClick.AddListener(OnPressStartButtonHandleClick);
            }
        }
        
        private void OnDisable()
        {
            _pressStartButton.onClick.RemoveListener(OnPressStartButtonHandleClick);
        }

        public void StartNetworkAsHost()
        {
            NetworkManager.Singleton.StartHost();
        }

        private void OnPressStartButtonHandleClick()
        {
            Debug.Log("HOST HERE");
            StartNetworkAsHost();
            
            _pressStartButton.gameObject.SetActive(false);
            _startNewGameButton.gameObject.SetActive(true);
        }

        private void OnNewGameStartButton()
        {
            _startNewGameButton.Select();
        }
    }
}