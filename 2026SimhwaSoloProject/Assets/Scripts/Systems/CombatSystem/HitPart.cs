
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Systems.CombatSystem
{
    public class HitPart : MonoBehaviour, IDamageable
    {
        [Header("히트 파트 연출값")] 
        [SerializeField] private float partHitPos;
        [SerializeField] private float hitMoveDelay;
        
        public UnityEvent<float> OnHitDamage;
        [SerializeField] private float damageMultiplier;
        
        private Sequence _hitSequence;

        public void TakeDamage(float damage)
        {
            OnHitDamage.Invoke(damage * damageMultiplier);

            if (_hitSequence != null && _hitSequence.IsActive())
                _hitSequence.Complete();

            _hitSequence = DOTween.Sequence();
            
            _hitSequence.Append(
                transform.DOMove(new Vector3(0, partHitPos, 0), hitMoveDelay).SetRelative(true));
            _hitSequence.Append(
                transform.DOMove(new Vector3(0, -partHitPos, 0), hitMoveDelay/2).SetRelative(true));
        }
    }
}
