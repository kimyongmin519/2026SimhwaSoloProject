using System;
using System.Collections;
using Core.Modules;
using UnityEngine;
using UnityEngine.Events;

namespace Weapons
{
    public abstract class Weapon : ModuleOwner
    {
        [field:SerializeField] public WeaponDataSO WeaponData { get; private set; }
        public WeaponRenderer Renderer { get; private set; }

        protected Vector2 _targetPos;

        public UnityEvent OnWeaponUseEvent;
        public UnityEvent OnWeaponEquipEvent;

        public bool CanUseWeapon { get; set; }
        protected WaitForSeconds _waitForSecWeapon;

        private event Action<bool> OnWeaponParentChange;
        public UnityEvent OnDropWeapon;
        public UnityEvent OnHandlingWeapon;

        [SerializeField] private Transform[] hands;

        private bool _isInHandler;
        public bool IsInHandler
        {
            get => _isInHandler;
            set
            {
                bool before = _isInHandler;
                _isInHandler = value;
                
                if (before != _isInHandler)
                    OnWeaponParentChange?.Invoke(value);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            Renderer = GetComponent<WeaponRenderer>();
        }

        private void Start()
        {
            OnWeaponParentChange += HandActive;
            
            IsInHandler = transform.parent != null;
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
            CanUseWeapon = false;
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
        }

        private void HandActive(bool value)
        {
            if (value)
            {
                WeaponSetting();
                OnHandlingWeapon?.Invoke();
            }
            else
            {
                OnDropWeapon?.Invoke();
                Debug.Log("lol");
            }
            
            foreach (Transform hand in hands)
            {
                hand.gameObject.SetActive(value);
            }
        }

        private void OnDestroy()
        {
            OnWeaponParentChange -= HandActive;
        }
    }
}