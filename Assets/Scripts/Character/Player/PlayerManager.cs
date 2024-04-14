using UnityEngine;

namespace UU
{
    public class PlayerManager : CharacterManager
    {
        [SerializeField] private PlayerLocomotionManager _playerLocomotionManager;
        
        private new void Awake()
        {
            base.Awake();
        }
    }
}