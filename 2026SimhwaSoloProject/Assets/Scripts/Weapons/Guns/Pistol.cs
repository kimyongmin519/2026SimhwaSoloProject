namespace Weapons.Guns
{
    public class Pistol : AbstractGun
    {
        public override void WeaponUse()
        {
            if (_isFiring || !CanUseWeapon) return;
            
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