using Systems.CombatSystem;
using UnityEngine;

namespace Weapon.Guns
{
    public class Pistol : AbstractGun
    {
        public override void WeaponUse()
        {
            if (_isFiring || !_canUseGun) return;

            BulletSpawn();
        }
        public override void WeaponEquip()
        {
            base.WeaponEquip();
            
            OnWeaponEquipEvent?.Invoke();
        }

        public override void WeaponServeSkill()
        {
            
        }

        public override void WeaponCancel()
        {
            
        }
    }
}