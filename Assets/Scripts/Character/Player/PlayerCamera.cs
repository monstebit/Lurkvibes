using System;
using UnityEngine;

namespace UU
{
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera Instance;

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
    }
}