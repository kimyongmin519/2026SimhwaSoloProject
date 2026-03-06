using System;
using UnityEngine;

namespace Weapons.Melees
{
    public class Knife : AbstractMeleeWeapon
    {

        public override void WeaponEquip()
        {
            base.WeaponEquip();
            OnWeaponEquipEvent?.Invoke();
        }

        public override void WeaponUse()
        {
            
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