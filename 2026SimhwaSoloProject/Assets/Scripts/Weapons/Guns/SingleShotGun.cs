using _06.GameLib.EventSystem;
using UnityEngine;

namespace Weapons.Guns
{
    public class SingleShotGun : AbstractGun
    {
        public override void WeaponUse()
        {
            if (_isFiring || !CanUseWeapon || _isReloading) return;
            
            BulletSpawn();
        }
        public override void WeaponEquip()
        {
            base.WeaponEquip();
            
            OnWeaponEquipEvent?.Invoke();
        }

        public override void WeaponServeSkill()
        {
            if (!CanUseWeapon) return;
        }

        public override void WeaponCancel()
        {
            
        }
    }
}