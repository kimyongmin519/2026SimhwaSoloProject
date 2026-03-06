using System;
using Systems.AnimationSystem;
using UnityEngine;

namespace Weapons
{
    public class WeaponRenderer : MonoBehaviour, IRenderer, IAnimationTrigger
    {
        [SerializeField] private AnimParamSO defaultAnimParam;
        
        private Animator _animator;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayClip(AnimParamSO animParam)
        {
            PlayClip(animParam.HashValue);
        }

        public void PlayClip(int clipHash, int layer = -1, float normalPosition = Single.NegativeInfinity)
        {
            _animator.Play(clipHash, layer, normalPosition);
        }

        public void SetBool(AnimParamSO param, bool value)
        {
        }

        public void SetFloat(AnimParamSO param, float value)
        {
        }

        public void SetInt(AnimParamSO param, int value)
        {
        }

        public void SetTrigger(AnimParamSO param)
        {
        }

        public event Action OnAnimationAttackTrigger;
        public event Action OnAnimationEndTrigger;
        public event Action OnAttackEndTrigger;

        public void AnimationAttackTrigger()
        {
            OnAnimationAttackTrigger?.Invoke();
        }

        public void AnimationEndTrigger()
        {
            PlayClip(defaultAnimParam.HashValue);
            OnAnimationEndTrigger?.Invoke();
        }

        public void AttackEndTrigger()
        {
            OnAttackEndTrigger?.Invoke();
        }
        public event Action OnSpecialTrigger;
        public void SpecialTrigger()
        {
            OnSpecialTrigger?.Invoke();   
        }
    }
}