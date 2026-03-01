using Agents.FSM;
using UnityEngine;

namespace Agents.Players.States
{
    public class PlayerJumpState : AbstractPlayerAirState
    {
        public PlayerJumpState(Agent owner) : base(owner)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            
            _mover.StopImmediately(false, true);
            _mover.AddForceToAgent(Vector2.up * _player.JumpPower);

            _mover.OnVelocityChange += HandleVelocityChanged;
        }

        private void HandleVelocityChanged(Vector2 velocity)
        {
            if (velocity.y < 0)
            {
                _player.ChangeState(PlayerStateEnum.FALL);
            }
        }

        public override void Exit()
        {
            base.Exit();
            
            _mover.OnVelocityChange -= HandleVelocityChanged;
        }
    }
}