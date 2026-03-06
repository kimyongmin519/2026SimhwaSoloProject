using System;
using _06.GameLib.ObjectPool.Runtime;
using UnityEngine;

namespace Systems.CombatSystem.EffectSystem
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorEffect : MonoBehaviour, IPoolable
    {
        [SerializeField] private bool destroyOnFinish = true;
        private Animator _animator;
        private float _startTime;
        private float _lifeTime;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayerEffectClip(int clipHash, float lifeTime)
        {
            _animator.Play(clipHash);
        }

        private void Update()
        {
            if (!destroyOnFinish) return;
            
            float passedTime = Time.time - _startTime;
            if (passedTime >= _lifeTime)
                Destroy(gameObject);
        }

        [field:SerializeField] public PoolItemSO PoolItem { get; set; }
        public GameObject GameObject => gameObject;
        public void ResetItem()
        {
            
        }
    }
}