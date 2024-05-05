using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace UU
{
    public class WorldSaveGameManager : MonoBehaviour
    {
        [SerializeField] private int _worldSceneIndex = 1;
        
        public static WorldSaveGameManager Instance;
        public int WorldSceneIndex => _worldSceneIndex;

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

        public IEnumerator LoadNewGame()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(_worldSceneIndex);

            yield return null;
        }
    }
}