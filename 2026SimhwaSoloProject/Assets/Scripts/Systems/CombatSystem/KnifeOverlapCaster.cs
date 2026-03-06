using System;
using System.Collections;
using UnityEngine;
using Weapons.Melees;

namespace Systems.CombatSystem
{
    public class KnifeOverlapCaster : OverlapDamageCaster
    {
        private Knife _owner;
        private bool _bodyInKnife;

        [SerializeField] private Transform anchorParts;

        public void Initialize(Knife owner)
        {
            _owner = owner;
        }

        private void Start()
        {
            _owner.Renderer.OnAnimationAttackTrigger += HandleInKnife;
            _owner.Renderer.OnAttackEndTrigger += HandleOutKnife;
        }

        public override bool CastDamage(float damage, Vector2 knockBackForce)
        {
            Array.Clear(_hitResults, 0, _hitResults.Length);
            
            int cnt = castType switch
            {
                CastType.CIRCLE => Physics2D.OverlapCircle(transform.position, radius, contactFilter, _hitResults),
                CastType.BOX => Physics2D.OverlapBox(transform.position, boxSize,0, contactFilter, _hitResults), 
                _ => 0
            };

            for (int i = 0; i < cnt; i++)
            {
                if (_hitResults[i].TryGetComponent(out IDamageable damageable))
                {
                    StartCoroutine(InKnifeHitCor(_hitResults[i].transform.root));
                    damageable.TakeDamage(damage);
                }
            }

            return cnt > 0;
        }

        private IEnumerator InKnifeHitCor(Transform target)
        {
            while (_bodyInKnife)
            {
                target.position = anchorParts.position;
                target.eulerAngles = anchorParts.eulerAngles;
                yield return null;
            }
            target.rotation = Quaternion.Euler(Vector3.zero);
        }

        private void HandleInKnife() => _bodyInKnife = true;
        private void HandleOutKnife() => _bodyInKnife = false;

        private void OnDestroy()
        {
            _owner.Renderer.OnAnimationAttackTrigger += HandleInKnife;
            _owner.Renderer.OnAttackEndTrigger += HandleOutKnife;
        }
    }
}