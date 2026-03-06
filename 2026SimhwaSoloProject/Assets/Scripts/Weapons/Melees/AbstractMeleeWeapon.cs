using System.Collections;
using Systems.CombatSystem;
using UnityEngine;

namespace Weapons.Melees
{
    public abstract class AbstractMeleeWeapon : Weapon
    {
        protected MeleeWeaponDataSO _meleeWeaponData;
        protected OverlapDamageCaster _damageCaster;

        private bool _bodyInKnife;

        protected override void Awake()
        {
            base.Awake();
            
            _meleeWeaponData = WeaponData as MeleeWeaponDataSO;
            Debug.Assert(_meleeWeaponData != null, "_meleeWeaponData not casting");
            _damageCaster = GetComponentInChildren<OverlapDamageCaster>();
            Debug.Assert(_damageCaster != null, "_damageCaster != null");
            
            _waitForSecWeapon = new WaitForSeconds(_meleeWeaponData.EquipAnimClip.length);
        }

        private void Start()
        {
            Renderer.OnAnimationAttackTrigger += HandleAttackCast;
            Renderer.OnAnimationAttackTrigger += HandleKnifeIn;
            Renderer.OnAttackEndTrigger += HandleKnifeOut;
            Renderer.OnAnimationEndTrigger += HandleAttackEndAnim;
        }

        protected void HandleAttackCast()
        {
            StartCoroutine(CastDamageCor());
        }

        private IEnumerator CastDamageCor()
        {
            yield return null;
            while (_bodyInKnife)
            {
                bool hit = _damageCaster.CastDamage(_meleeWeaponData.Damage, transform.right);
                
                if (hit)
                    break;
                
                yield return null;
            }
        }
        
        private void OnDestroy()
        {
            Renderer.OnAnimationAttackTrigger -= HandleAttackCast;
            Renderer.OnAnimationAttackTrigger -= HandleKnifeIn;
            Renderer.OnAttackEndTrigger -= HandleKnifeOut;
            Renderer.OnAnimationEndTrigger -= HandleAttackEndAnim;
        }

        private void HandleAttackEndAnim() => CanUseWeapon = true;
        private void HandleKnifeIn() => _bodyInKnife = true;
        private void HandleKnifeOut() => _bodyInKnife = false;
    }
}
