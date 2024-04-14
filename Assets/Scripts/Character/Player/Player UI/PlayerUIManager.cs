using Unity.Netcode;
using UnityEngine;

namespace UU
{
    public class PlayerUIManager : MonoBehaviour
    {
        public static PlayerUIManager Instance;
        
        [Header("NETWORK JOIN")] 
        [SerializeField] private bool _startGameAsClient;

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
        }

        private void Update()
        {
            if (_startGameAsClient)
            {
                _startGameAsClient = false;
                //  WE MUST FIRST SHUT DOWN, BECAUSE WE HAVE STARTED AS A HOST DURING THE TITLE SCREEN
                NetworkManager.Singleton.Shutdown();
                //  WE THEN RESTART, AS A CLIENT
                NetworkManager.Singleton.StartClient();
            }
        }
    }
}