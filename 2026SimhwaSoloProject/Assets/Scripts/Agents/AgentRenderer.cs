using System;
using Core.Modules;
using Systems.AnimationSystem;
using UnityEngine;

namespace Agents
{
    [RequireComponent(typeof(Animator))]
    public class AgentRenderer : MonoBehaviour, IModule, IRenderer, IAnimationTrigger
    {
        private Agent _owner;
        private Animator _animator;

        public float FacingDirection { get; private set; } = 1f;
        
        public void Initialize(ModuleOwner owner)
        {
            _owner = owner as Agent;
            Debug.Assert(_owner != null, $"Failed owner casting. : {gameObject.name}");
            _animator = owner.GetComponent<Animator>();
        }
        
        public void PlayClip(int clipHash, int layer = -1, float normalPosition = float.NegativeInfinity)
            => _animator.Play(clipHash, layer, normalPosition);
        public void SetBool(AnimParamSO param, bool value) 
            => _animator.SetBool(param.HashValue, value);
        public void SetFloat(AnimParamSO param, float value) 
            => _animator.SetFloat(param.HashValue, value);
        public void SetInt(AnimParamSO param, int value) 
            => _animator.SetInteger(param.HashValue, value);
        public void SetTrigger(AnimParamSO param)
            => _animator.SetTrigger(param.HashValue);

        public void FlipController()
        {
            
        }

        public event Action OnAnimationAttackTrigger;
        public event Action OnAnimationEndTrigger;

        public void AnimationAttackTrigger()
        {
            OnAnimationAttackTrigger?.Invoke();
        }

        public void AnimationEndTrigger()
        {
            OnAnimationEndTrigger?.Invoke();
        }
    }
}
