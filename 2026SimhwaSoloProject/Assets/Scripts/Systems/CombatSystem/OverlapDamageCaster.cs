using System;
using UnityEngine;

namespace Systems.CombatSystem
{
    public class OverlapDamageCaster : AbstractDamageCaster
    {
        public enum CastType
        {
            CIRCLE, BOX
        }
        
        [SerializeField] protected CastType castType;
        [SerializeField] protected float radius;
        [SerializeField] protected Vector2 boxSize;
        
        protected Collider2D[] _hitResults;
        
        public void SetRadius(float r) => radius =  r;
        public void SetBoxSize(Vector2 size) => boxSize = size;

        private void Awake()
        {
            _hitResults = new Collider2D[maxHitCount];
        }

        public override bool CastDamage(float damage, Vector2 knockBackForce)
        {
            int cnt = castType switch
            {
                CastType.CIRCLE => Physics2D.OverlapCircle(transform.position, radius, contactFilter, _hitResults),
                CastType.BOX => Physics2D.OverlapBox(transform.position, boxSize,0, contactFilter, _hitResults), 
                _ => 0
            };

            for (int i = 0; i < cnt; i++)
            {
                if (_hitResults[i].transform.root.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(damage);
                    break;
                }
            }

            return cnt > 0;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            if (castType == CastType.CIRCLE)
                Gizmos.DrawWireSphere(transform.position, radius);
            else if (castType == CastType.BOX)
            {
                Gizmos.DrawWireCube(transform.position, boxSize);
            }
        }
    }
}
