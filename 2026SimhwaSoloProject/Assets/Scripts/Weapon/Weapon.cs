using System;
using System.Collections;
using Core.Modules;
using UnityEngine;
using UnityEngine.Events;

namespace Weapon
{
    public abstract class Weapon : ModuleOwner
    {
        [field:SerializeField] public WeaponDataSO WeaponData { get; private set; }
        protected WeaponRenderer _renderer;

        protected Vector2 _targetPos;

        public UnityEvent OnWeaponUseEvent;
        public UnityEvent OnWeaponEquipEvent;

        protected override void Awake()
        {
            base.Awake();
            _renderer = GetComponent<WeaponRenderer>();
            
            WeaponSetting();
        }

        public void SetTarget(Vector2 pos)
        {
            _targetPos = pos;
        }
        
        public virtual void WeaponSetting()
        {
            Vector3 parentPos = transform.parent.localPosition;
            transform.parent.localPosition = new Vector3(parentPos.x, WeaponData.PingHeight, parentPos.z);
        }

        public abstract void WeaponEquip();
        public abstract void WeaponUse();
        public abstract void WeaponServeSkill();
        public abstract void WeaponCancel();
    }
}