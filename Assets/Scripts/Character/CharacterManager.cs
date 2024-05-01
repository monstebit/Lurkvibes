using UnityEngine;

namespace UU
{
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField] public CharacterController CharacterController;
        
        protected void Awake()
        {
            // Находим корневой объект
            GameObject rootObject = this.transform.root.gameObject;
            
            // DontDestroyOnLoad(this);
            DontDestroyOnLoad(this);
        }

        protected void Update() { }
    }
}