using Agents.FSM;

namespace Agents.Players.States
{
    public class PlayerFallState : AbstractPlayerAirState
    {
        public PlayerFallState(Agent owner) : base(owner)
        {
            
        }

        public override void Update()
        {
            base.Update();
            
            if (_player.Mover.IsGrounded)
                _player.ChangeState(PlayerStateEnum.IDLE);
        }
    }
}