using Systems.GameEvents;
using System;
using System.Collections;
using _06.GameLib.ObjectPool.Runtime;
using Agents;
using Agents.Players;
using Systems.AnimationSystem;
using Systems.GameEvents.ChannelEvent;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Systems.CombatSystem
{
    public class Bullet : PoolMono
    {
        [Header("총알 세팅")]
        [SerializeField] private float speed;
        [SerializeField] private float bulletHitRadius;
        [SerializeField] private Collider2D bulletCollider;
        [SerializeField] private float bulletLifeTime;
        
        [Header("필요한 것들")]
        [SerializeField] private EventChannelSO createChannel;
        [SerializeField] private AnimParamSO hitEffectParam;
        [SerializeField] private PoolItemSO bloodPoolItem;
        [SerializeField] private PoolManagerSO poolManager;
        [SerializeField] private LayerMask layerMask;

        private Rigidbody2D _rb;
        private TrailRenderer _trailRenderer; 
        
        public UnityEvent OnSuccessHit;
        
        private WaitForSeconds _waitForSeconds;
        private float _damage;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _trailRenderer = GetComponent<TrailRenderer>();
            
            _waitForSeconds = new WaitForSeconds(bulletLifeTime);
        }

        private void OnDisable()
        {
            _trailRenderer.enabled = false;
        }

        public void Shoot(float damage ,Vector2 dir, Vector2 targetPos)
        {
            _damage = damage;
            
            _trailRenderer.enabled = true;
            
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            
            transform.rotation = Quaternion.Euler(0, 0, angle);
            _rb.linearVelocity = dir.normalized * speed;
            
            StartCoroutine(LifeTimeCoroutine());
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log(other.collider.name);
            
            float rotationValue = Random.Range(0, 360);
            
            createChannel.RaiseEvent(CreateEvents.CreateEffect.Init(transform.position, Quaternion.Euler(0,0,rotationValue)
                    , hitEffectParam.HashValue, true));
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1, layerMask);

            if (hit.collider != null)
            {
                if (hit.collider.transform.TryGetComponent(out IDamageable damageable))
                {
                    float bloodDirection = hit.normal.x > 0 ? 90 : 270;
                    
                    createChannel.RaiseEvent(CreateEvents.CreateParticle.Init(hit.point, Quaternion.Euler(0,bloodDirection,0)
                    , bloodPoolItem, true ));

                    damageable.TakeDamage(_damage);
                }
            }
            
            ResetItem();
            poolManager.Push(this);
        }

        private IEnumerator LifeTimeCoroutine()
        {
            yield return _waitForSeconds;
            poolManager.Push(this);
        }
    }
}
