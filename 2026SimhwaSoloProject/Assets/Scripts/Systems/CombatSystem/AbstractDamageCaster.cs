using Core.Modules;
using UnityEngine;

namespace Systems.CombatSystem
{
    public abstract class AbstractDamageCaster : MonoBehaviour
    {
        [SerializeField] protected int maxHitCount = 5;
        [SerializeField] protected ContactFilter2D contactFilter;
        
        public abstract bool CastDamage(float damage, Vector2 knockbackForce);
    }
}
