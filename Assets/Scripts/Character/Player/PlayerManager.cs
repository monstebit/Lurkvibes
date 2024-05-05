using UnityEngine;

namespace UU
{
    public class PlayerManager : CharacterManager
    {
        [SerializeField] private PlayerLocomotionManager _playerLocomotionManager;
        
        // private new void Awake()
        protected override void Awake()
        {
            base.Awake();
        }

        // private new void Update()
        protected override void Update()
        {
            base.Update();
            
            //  ЕСЛИ МЫ НЕ ВЛАДЕЕМ ЭТИМ GAMEOBJECT, МЫ НЕ МОЖЕМ РЕДАКТИРОВАТЬ ИЛИ КОНТРОЛИРОВАТЬ ЕГО
            if (!IsOwner)
                return;
            
            _playerLocomotionManager.HandleAllMovement();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
        }
    }
}