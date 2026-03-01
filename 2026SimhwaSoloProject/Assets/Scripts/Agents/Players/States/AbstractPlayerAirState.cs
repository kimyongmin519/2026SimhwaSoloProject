namespace Agents.Players.States
{
    public abstract class AbstractPlayerAirState : AbstractPlayerState
    {
        public AbstractPlayerAirState(Agent owner) : base(owner)
        {
            
        }

        public override void Update()
        {
            base.Update();
            float xInput = _player.PlayerInput.InputDirection.x;
            _mover.SetMovement(xInput);
        }
    }
}