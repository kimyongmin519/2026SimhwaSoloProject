using Agents.FSM;
using Agents.Players.States;
using Core;
using Core.Modules;
using Systems;
using UnityEngine;
using Weapon;

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
        private PlayerWeaponHandler _handler;
        
        #endregion

        protected override void InitializeComponents()
        {
            base.InitializeComponents();
            _stateMachine = new AgentStateMachine(this, stateList.states);
            _handler = GetModule<PlayerWeaponHandler>();
            Debug.Assert(_handler != null, "Player handler is null");
        }

        protected override void AfterInitializeComponents()
        {
            base.AfterInitializeComponents();
            PlayerInput.OnJumpPressed += HandleJumpKeyPressed;
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
            
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(PlayerInput.ScreenMousePos);
            
            trackingOwner.SetTrackingPos(mousePos);
        }
        
        public void ChangeState(PlayerStateEnum nextState) => _stateMachine.ChangeState((int)nextState);
        public AgentState GetCurrentState() => _stateMachine.CurrentState;
    }
}