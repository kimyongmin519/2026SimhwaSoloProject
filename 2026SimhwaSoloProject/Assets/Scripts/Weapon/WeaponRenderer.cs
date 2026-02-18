using System;
using Core.Modules;
using Systems.AnimationSystem;
using TMPro;
using UnityEngine;

namespace Weapon
{
    public class WeaponRenderer : MonoBehaviour, IRenderer, IModule, IAnimationTrigger
    {
        [SerializeField] private AnimParamSO defaultAnimParam;
        
        private Animator _animator;
        private ModuleOwner _owner;

        public void Initialize(ModuleOwner owner)
        {
            _owner = owner;
        }
        private void Awake()
        {
            _animator = GetComponent<Animator>();
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
        
        public void AnimationEndTrigger()
        {
            PlayClip(defaultAnimParam.HashValue);
        }
    }
}