using System;
using _06.GameLib.ObjectPool.Runtime;
using _06.GameLib.Runtime;
using Core.Modules;
using UnityEngine;

namespace Systems.CombatSystem
{
    public class Bullet : AbstractDamageCaster, IPoolable
    {
        [Header("총알 값")]
        [SerializeField] private float speed;
        [SerializeField] private float bulletHitRadius;
        [SerializeField] private Collider2D bulletCollider;
        
        private Collider2D[] _hitResults;
        [SerializeField] private GameObject testEffect;

        private Rigidbody2D _rb;
        private TrailRenderer _trailRenderer;
        
        private bool _successHit;

        private void Awake()
        {
            _hitResults = new Collider2D[maxHitCount];
            _rb = GetComponent<Rigidbody2D>();
            _trailRenderer = GetComponent<TrailRenderer>();
        }

        private void OnDisable()
        {
            _trailRenderer.enabled = false;
        }

        public void Shoot(Vector2 dir, Vector2 targetPos)
        {
            _trailRenderer.enabled = true;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            
            transform.rotation = Quaternion.Euler(0,0,angle);
            _rb.linearVelocity = dir * speed;

            CastDamage(5, Vector2.right, targetPos);
        }
        
        public override bool CastDamage(float damage, Vector2 knockbackForce, Vector2 hitPoint)
        {
            Array.Clear(_hitResults, 0, _hitResults.Length);
            
            _successHit = false;
            bulletCollider.isTrigger = false;
            
            Physics2D.OverlapCircle(hitPoint, bulletHitRadius, contactFilter, _hitResults);

            foreach (Collider2D hit in _hitResults)
            {
                if (hit == null) continue;
                
                if (hit.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(damage);
                    _successHit = true;
                }
            }

            if (!_successHit)
                bulletCollider.isTrigger = true;

            return _successHit;
        }

        [field:SerializeField] public PoolItemSO PoolItem { get; set; }
        public GameObject GameObject => gameObject;
        public void ResetItem()
        {
            
        }
    }
}
