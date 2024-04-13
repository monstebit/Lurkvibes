using System;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

namespace UU
{
    public class TitleScreenManager : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        
        private void Start()
        {
            if (_startButton != null)
            {
                _startButton.onClick.AddListener(OnStartButtonHandleClick);
            }
        }

        public void StartNetworkAsHost()
        {
            NetworkManager.Singleton.StartHost();
        }

        private void OnStartButtonHandleClick()
        {
            StartNetworkAsHost();
            Debug.Log("HOST HERE");
        }
    }
}