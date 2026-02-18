using Agents.FSM;
using Core;
using Systems;
using UnityEngine;
using Weapon;

namespace Agents.Players
{
    public class Player : Agent
    {
        [field:SerializeField] public PlayerInputSO PlayerInput { get; private set; }
        [SerializeField] private StateListSO stateList;

        private AgentStateMachine _stateMachine;

        #region 플레이어 부착

        [SerializeField] private TrackingOwner trackingOwner; 
        private WeaponHandler _handler;
        
        #endregion

        protected override void InitializeComponents()
        {
            base.InitializeComponents();
            _stateMachine = new AgentStateMachine(this, stateList.states);
            _handler = GetModule<WeaponHandler>();
            Debug.Assert(_handler != null, "Player handler is null");
            _handler.Initialize(this);
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