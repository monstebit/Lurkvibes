using UnityEngine;
using Unity.Netcode;

namespace UU
{
    public class CharacterManager : NetworkBehaviour
    {
        [SerializeField] public CharacterController CharacterController;
        [SerializeField] public CharacterNetworkManager CharacterNetworkManager;
        
        protected void Awake()
        {
            // Находим корневой объект
            GameObject rootObject = this.transform.root.gameObject;
            
            // DontDestroyOnLoad(this);
            DontDestroyOnLoad(this);
        }

        protected void Update()
        {
            if (IsOwner)
            {
                CharacterNetworkManager.NetworkPosition.Value = transform.position;
                CharacterNetworkManager.NetworkRotation.Value = transform.rotation;
            }
            else
            {
                //  Position
                transform.position = Vector3.SmoothDamp(
                    transform.position,
                    CharacterNetworkManager.NetworkPosition.Value,
                    ref CharacterNetworkManager.NetworkPositionVelocity,
                    CharacterNetworkManager.NetworkPositionSmoothTime);
                
                //  Rotation
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    CharacterNetworkManager.NetworkRotation.Value,
                    CharacterNetworkManager.NetworkRotationSmoothTime);
            }
        }
    }
}