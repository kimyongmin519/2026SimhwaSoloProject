using System.Collections;
using _06.GameLib.EventSystem;
using _06.GameLib.ObjectPool.Runtime;
using GameSystems.AnimationSystem;
using GameSystems.GameEvents.ChannelEvent;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace GameSystems.CombatSystem
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
        private float _headDamage;

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

        public void Shoot(float damage ,float headDamage ,Vector2 dir, Vector2 targetPos)
        {
            _damage = damage;
            _headDamage = headDamage;
            
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            
            transform.rotation = Quaternion.Euler(0, 0, angle);
            _rb.linearVelocity = dir.normalized * speed;
            
            StartCoroutine(LifeTimeCoroutine());
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            float rotationValue = Random.Range(0, 360);
            
            createChannel.RaiseEvent(CreateEvents.CreateEffect.Init(transform.position, Quaternion.Euler(0,0,rotationValue)
                    , hitEffectParam.HashValue, true));
            
            if (other.collider != null)
            {
                if (other.collider.transform.TryGetComponent(out IDamageable damageable))
                {
                    float bloodDirection = other.transform.position.x < transform.position.x ? 90 : 270;
                    
                    createChannel.RaiseEvent(CreateEvents.CreateParticle.Init(transform.position, Quaternion.Euler(0,bloodDirection,0)
                    , bloodPoolItem, true ));


                    if (damageable is HitPart hitPart)
                    {
                        if (hitPart.IsHead)
                            hitPart.TakeDamage(_headDamage);
                        else
                        {
                            hitPart.TakeDamage(_damage);
                        }
                    }
                    else
                    {
                        damageable.TakeDamage(_damage);
                    }
                }
            }
            
            ResetItem();
            poolManager.Push(this);
        }

        private IEnumerator LifeTimeCoroutine()
        {
            _trailRenderer.enabled = true;
            yield return _waitForSeconds;
            poolManager.Push(this);
        }
    }
}
