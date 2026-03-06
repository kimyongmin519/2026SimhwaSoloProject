using System;
using Agents.FSM;
using Agents.Players.States;
using Core;
using Systems;
using UnityEngine;
using Weapons;
using Weapons.Melees;

namespace Agents.Players
{
    public class Player : Agent
    {
        [field:SerializeField,Header("플레이어 값")]
        public float JumpPower { get; private set; }
        
        [field:SerializeField] public PlayerInputSO PlayerInput { get; private set; }
        [SerializeField] private StateListSO stateList;

        private AgentStateMachine _stateMachine;

        #region 플레이어 부착

        [SerializeField] private TrackingOwner trackingOwner; 
        public PlayerWeaponHandler Handler { get; private set; }
        public IMover Mover { get; private set; }
        
        #endregion

        protected override void InitializeComponents()
        {
            base.InitializeComponents();
            _stateMachine = new AgentStateMachine(this, stateList.states);
            Handler = GetModule<PlayerWeaponHandler>();
            Mover = GetModule<IMover>();
            Debug.Assert(Handler != null, "Player handler is null");
        }

        protected override void AfterInitializeComponents()
        {
            base.AfterInitializeComponents();
            PlayerInput.OnJumpPressed += HandleJumpKeyPressed;
            PlayerInput.OnAttackPressed += HandleMeleeAttack;
            Handler.OnWeaponChanged += HandleChangeWeapon;
            
        }

        private void HandleJumpKeyPressed()
        {
            if (_stateMachine.CurrentState is ICanJumpState)
                ChangeState(PlayerStateEnum.JUMP);
        }

        protected override void Start()
        {
            base.Start();
            ChangeState(PlayerStateEnum.IDLE);
        }

        private void Update()   
        {
            _stateMachine.UpdateMachine();
            
            Vector2 mousePos = PlayerInput.MousePos;
            
            trackingOwner.SetTrackingPos(mousePos);
        }

        private void HandleChangeWeapon(float multifier)
        {
            ChangeState(PlayerStateEnum.IDLE);
            Mover.SetMoveSpeedMultiplier(multifier);
        }

        private void HandleMeleeAttack()
        {
            Weapon currentWeapon = Handler.CurrentWeaponClass;

            if (currentWeapon.WeaponData.WeaponType == WeaponType.MELEE
                && currentWeapon.CanUseWeapon)
            {
                currentWeapon.CanUseWeapon = false;
                ChangeState(PlayerStateEnum.MELEE_ATTACK);
            }
        }
        
        public void ChangeState(PlayerStateEnum nextState) => _stateMachine.ChangeState((int)nextState);
        public AgentState GetCurrentState() => _stateMachine.CurrentState;

        private void OnDestroy()
        {
            PlayerInput.OnJumpPressed -= HandleJumpKeyPressed;
            PlayerInput.OnAttackPressed -= HandleMeleeAttack;
            Handler.OnWeaponChanged -= HandleChangeWeapon;
        }
    }
}