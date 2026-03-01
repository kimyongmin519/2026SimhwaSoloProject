using Agents.FSM;
using UnityEngine;

namespace Agents.Players.States
{
    public class PlayerIdleState : AbstractPlayerState, ICanJumpState
    {
        public PlayerIdleState(Agent owner) : base(owner)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _mover.StopImmediately(true,false);
        }

        public override void Update()
        {
            base.Update();
            float xInput = _player.PlayerInput.InputDirection.x;

            if (Mathf.Abs(xInput) > 0.1f)
            {
                _player.ChangeState(PlayerStateEnum.MOVE);
            }
        }
    }
}