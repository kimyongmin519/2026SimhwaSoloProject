using System;
using System.Collections;
using _06.GameLib.EventSystem;
using Core.Modules;
using UnityEngine;
using UnityEngine.Events;

namespace Weapons
{
    public abstract class Weapon : ModuleOwner
    {
        [field:SerializeField] public WeaponDataSO WeaponData { get; private set; }
        [SerializeField] protected EventChannelSO playerUIChannel;
        public WeaponRenderer Renderer { get; private set; }

        protected Vector2 _targetPos;

        public UnityEvent OnWeaponUseEvent;
        public UnityEvent OnWeaponEquipEvent;
        protected Action OnCanUseWeapon;

        public bool CanUseWeapon { get; set; }
        protected WaitForSeconds _waitForSecWeapon;

        protected override void Awake()
        {
            base.Awake();
            Renderer = GetComponent<WeaponRenderer>();
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

        public virtual void WeaponEquip()
        {
            StartCoroutine(CanUseWeaponCor());
        }

        public abstract void WeaponUse();

        public abstract void WeaponServeSkill();

        public abstract void WeaponCancel();
        
        protected IEnumerator CanUseWeaponCor()
        {
            CanUseWeapon = false;
            yield return _waitForSecWeapon;
            CanUseWeapon =  true;
            OnCanUseWeapon?.Invoke();
        }
    }
}