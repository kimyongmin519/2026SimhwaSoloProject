using UnityEngine;
using UnityEngine.Events;

namespace GameSystems.CombatSystem
{
    public class HitPartOwner : MonoBehaviour, IDamageable
    {
        public UnityEvent<float> OnHitDamage;
        public void TakeDamage(float damage)
        {
            OnHitDamage.Invoke(damage);
        }
    }
}
