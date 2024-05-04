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

        // protected virtual void Update()
        private new void Update()
        {
            base.Update();
            
            //  ЕСЛИ МЫ НЕ ВЛАДЕЕМ ЭТИМ GAMEOBJECT, МЫ НЕ МОЖЕМ РЕДАКТИРОВАТЬ ИЛИ КОНТРОЛИРОВАТЬ ЕГО
            if (!IsOwner)
                return;
            
            _playerLocomotionManager.HandleAllMovement();
        }
    }
}