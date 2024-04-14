using UnityEngine;

namespace UU
{
    public class CharacterManager : MonoBehaviour
    {
        protected void Awake()
        {
            // Находим корневой объект
            GameObject rootObject = this.transform.root.gameObject;
            
            // DontDestroyOnLoad(this);
            DontDestroyOnLoad(this);
        }
    }
}