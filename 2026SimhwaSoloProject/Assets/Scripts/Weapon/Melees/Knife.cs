
namespace Weapon.Melees
{
    public class Knife : AbstractMeleeWeapon
    {
        public override void WeaponEquip()
        {
            OnWeaponEquipEvent?.Invoke();
        }

        public override void WeaponUse()
        {
            OnWeaponUseEvent?.Invoke();
        }

        public override void WeaponServeSkill()
        {
        }

        public override void WeaponCancel()
        {
        }
    }
}