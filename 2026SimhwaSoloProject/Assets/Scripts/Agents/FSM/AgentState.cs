using Systems.AnimationSystem;
using UnityEditor;
using UnityEngine;

namespace Agents.FSM
{
    public class AgentState
    {
        protected Agent _owner;
        protected bool _isTriggerCall;

        protected IRenderer _renderer;

        public AgentState(Agent owner)
        {
            _owner = owner;
            _renderer = owner.GetModule<IRenderer>();
        }

        public virtual void Update()
        {
            
        }

        public virtual void Enter()
        {
            _isTriggerCall = false;
        }

        public virtual void Exit()
        {
            
        }

        public void AnimationEndTrigger() => _isTriggerCall = true;
    }
}