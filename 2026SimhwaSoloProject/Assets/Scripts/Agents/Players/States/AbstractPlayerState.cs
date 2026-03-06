using Agents.FSM;
using UnityEngine;

namespace Agents.Players.States
{
    public abstract class AbstractPlayerState : AgentState
    {
        protected Player _player;
        
        public AbstractPlayerState(Agent owner) : base(owner)
        {
            _player = owner as Player;
            Debug.Assert(_player != null, "Player null");
        }
    }
}