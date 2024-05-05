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

        protected override void LateUpdate()
        {
            if (!IsOwner)
                return;
            
            base.LateUpdate();
            
            PlayerCamera.Instance.HandleAllCameraActions();
        }

        // Если текущий объект является владельцем (например, игрок, управляемый этим клиентом),
        // то устанавливается камера игрока в качестве камеры игрока.
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            
            if (IsOwner)
            {
                //  ИНИЦИАЛИЗАЦИЯ Player В ИНСПЕКТОР PlayerCamera
                PlayerCamera.Instance.Player = this;
            }
        }
    }
}