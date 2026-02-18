using System;
using Core.Modules;
using UnityEngine;

namespace Weapon
{
    public abstract class Weapon : ModuleOwner
    {
        [field:SerializeField] public WeaponDataSO WeaponData { get; private set; }
        protected WeaponRenderer _renderer;

        protected override void Awake()
        {
            base.Awake();
            _renderer = GetComponent<WeaponRenderer>();
        }

        public abstract void WeaponUse();
        public abstract void WeaponServeSkill();
        public abstract void WeaponCancel();
    }
}