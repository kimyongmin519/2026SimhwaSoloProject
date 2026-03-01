using Agents.FSM;
using Systems.AnimationSystem;
using UnityEngine;

namespace Agents.Players.States
{
    public class PlayerMoveState : AbstractPlayerState, ICanJumpState
    {
        public PlayerMoveState(Agent owner) : base(owner)
        {
        }

        public override void Update()
        {
            base.Update();
            float xInput = _player.PlayerInput.InputDirection.x;
            
            _mover.SetMovement(xInput);

            if (Mathf.Approximately(xInput, 0f))
            {
                _player.ChangeState(PlayerStateEnum.IDLE);
            }
            
            if(!_mover.IsGrounded)
                _player.ChangeState(PlayerStateEnum.FALL);
        }
    }
}