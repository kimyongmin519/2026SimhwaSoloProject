using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystems.CombatSystem
{
    public class HitPart : MonoBehaviour, IDamageable
    {
        [Header("히트 파트 연출값")] 
        [SerializeField] private float partHitPos;
        [SerializeField] private float hitMoveDelay;

        [field:SerializeField,Header("설정값")] 
        public bool IsHead { get; private set; }
        
        
        public UnityEvent<float> OnHitDamage;
        
        private Sequence _hitSequence;

        public void TakeDamage(float damage)
        {
            OnHitDamage.Invoke(damage);

            HitDirecting();
        }

        public void HitDirecting()
        {
            if (_hitSequence != null && _hitSequence.IsActive())
                _hitSequence.Complete();

            _hitSequence = DOTween.Sequence();
            
            _hitSequence.Append(
                transform.DOLocalMove(new Vector3(0, partHitPos, 0), hitMoveDelay).SetRelative(true));
            _hitSequence.Append(
                transform.DOLocalMove(new Vector3(0, -partHitPos, 0), hitMoveDelay/2).SetRelative(true));
        }
    }
}
