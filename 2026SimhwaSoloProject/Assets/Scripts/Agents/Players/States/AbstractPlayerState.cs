using Agents.FSM;
using UnityEngine;

namespace Agents.Players.States
{
    public class AbstractPlayerState : AgentState
    {
        protected IMover _mover;
        protected Player _player;
        
        public AbstractPlayerState(Agent owner) : base(owner)
        {
            _player = owner as Player;
            Debug.Assert(_player != null, "Player null");
            _mover = owner.GetModule<IMover>();
        }
    }
}