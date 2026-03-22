using System;
using System.Collections;
using _06.GameLib.ObjectPool.Runtime;
using Core;
using GameSystems.CombatSystem;
using GameSystems.GameEvents.BusEvent;
using GameSystems.GameEvents.BusEvent.BusEvents;
using GameSystems.GameEvents.ChannelEvent;
using UnityEngine;
using UnityEngine.Events;

namespace Weapons.Guns
{
    public abstract class AbstractGun : Weapon
    {
        [SerializeField] protected Transform firePos;
        [SerializeField] protected Transform magsPos;
        [SerializeField] protected PoolManagerSO poolManager;
        [SerializeField] private PoolItemSO bulletPoolData;
        [SerializeField] private Mags magPrefab;

        public UnityEvent OnGunReload;
        
        protected GunDataSO _gunData;
        protected NotifyValue<int> _currentAmmo;
        private int _leftAmmo;

        public int LeftAmmo
        {
            get => _leftAmmo;
            set
            {
                _leftAmmo = Mathf.Clamp(value, 0, _gunData.MaxAmmo);
            }
        }

        protected bool _isFiring = false;
        protected bool _isReloading = false;

        private WaitForSeconds _waitForFireRate;
        private WaitForSeconds _waitForReload;

        protected override void Awake()
        {
            base.Awake();
            _gunData = WeaponData as GunDataSO;
            Debug.Assert(Renderer != null, "Gun Renderer is null");
            Debug.Assert(_gunData != null, "Weapon data not casting Gun data");

            LeftAmmo = _gunData.MaxAmmo;
            _currentAmmo = new NotifyValue<int>(_gunData.Ammo);

            OnCanUseWeapon += GunDefaultSetting;
            
            _waitForSecWeapon = new WaitForSeconds(_gunData.EquipAnimClip.length);
            _waitForFireRate = new WaitForSeconds(_gunData.ShotAnimClip.length);
            _waitForReload = new WaitForSeconds(_gunData.ReloadAnimClip.length);
        }

        private void Start()
        {
            _currentAmmo.OnValueChanged += HandleAmmoValueChange;
        }

        private void HandleAmmoValueChange(int before, int after)
        {
            playerUIChannel.RaiseEvent(PlayerUIEvent.GunValueChanged.Init(_currentAmmo.Value, LeftAmmo));
        }

        public override void WeaponEquip()
        {
            base.WeaponEquip();
            HandleAmmoValueChange(_currentAmmo.Value, LeftAmmo);
        }

        protected bool BulletSpawn()
        {
            if (_currentAmmo.Value <= 0)
            {
                GunReload();
                return false;
            }
            
            StartCoroutine(ShotDelayCor());
                
            if (_currentAmmo.Value <= 0)
            {
                return false;
            }
            else
            {
                OnWeaponUseEvent?.Invoke();
                KimBus<CameraShakeEvent>.RaiseEvent(new CameraShakeEvent(_gunData.ShotImpulse));
                Bullet bullet = poolManager.Pop<Bullet>(bulletPoolData);
                bullet.transform.position = firePos.position;
                bullet.Shoot(_gunData.Damage , _gunData.HeadShotDamage,transform.right, _targetPos);

                _currentAmmo.Value -= 1;
                
                return true;
            }
        }

        public virtual void GunReload()
        {
            if (LeftAmmo <= 0) return;
            
            OnGunReload?.Invoke();
            StartCoroutine(ReloadCor());
        }
        public void MagsDrop()
        {
            Mags mags = Instantiate(magPrefab, magsPos.position, Quaternion.identity);
            mags.InitializePart(_gunData.MagsSprite);
        }

        private IEnumerator ShotDelayCor()
        {
            _isFiring = true;
            yield return _waitForFireRate;
            _isFiring = false;
        }

        public void AmmoReload()
        {
            LeftAmmo += _currentAmmo.Value;
            LeftAmmo -= _gunData.Ammo;
            
            _currentAmmo.Value = _gunData.Ammo;
        }

        private IEnumerator ReloadCor()
        {
            _isReloading = true;
            yield return _waitForSecWeapon;
            _isReloading = false;
        }

        private void GunDefaultSetting()
        {
            StopAllCoroutines();
            _isFiring = false;
            _isReloading = false;
        }

        private void OnDestroy()
        {
            OnCanUseWeapon -= GunDefaultSetting;
            _currentAmmo.OnValueChanged -= HandleAmmoValueChange;
        }
    }
}
