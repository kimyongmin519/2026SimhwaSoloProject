using Core.Modules;
using UnityEngine;

namespace Systems.CombatSystem
{
    public abstract class AbstractDamageCaster : MonoBehaviour
    {
        public ModuleOwner Owner { get; private set; }

        [SerializeField] protected int maxHitCount = 5;
        [SerializeField] protected ContactFilter2D contactFilter;
        

        public virtual void InitCaster(ModuleOwner owner)
        {
            Owner = owner;
        }
        
        public abstract bool CastDamage(float damage, Vector2 knockbackForce, Vector2 hitPoint);
    }
}
