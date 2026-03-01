using System.Collections;
using _06.GameLib.ObjectPool.Runtime;
using _06.GameLib.Runtime;
using Core;
using Systems.CombatSystem;
using UnityEngine;

namespace Weapon.Guns
{
    public abstract class AbstractGun : Weapon
    {
        [SerializeField] protected Transform firePos;
        [SerializeField] protected PoolManagerSO poolManager;
        [SerializeField] private PoolItemSO bulletPoolData;
        
        protected GunDataSO _gunData;
        protected NotifyValue<int> _currentAmmo;
        private int _oneMagazineAmmo;

        protected float _fireRate;

        protected bool _isFiring = false;
        
        private WaitForSeconds _waitForSeconds;
        private WaitForSeconds _waitForGunSettingSeconds;

        protected bool _canUseGun;

        protected override void Awake()
        {
            base.Awake();
            _gunData = WeaponData as GunDataSO;
            Debug.Assert(_renderer != null, "Gun Renderer is null");
            Debug.Assert(_gunData != null, "Weapon data not casting Gun data");
            
            _oneMagazineAmmo = _gunData.Ammo;
            _currentAmmo = new NotifyValue<int>(_oneMagazineAmmo);
            _fireRate = _gunData.ShotAnimClip.length;

            _waitForSeconds = new WaitForSeconds(_fireRate);
            _waitForGunSettingSeconds = new WaitForSeconds(_gunData.EquipAnimClip.length);
        }

        protected IEnumerator FireCoroutine(Bullet bullet)
        {
            _isFiring = true;
            yield return _waitForSeconds;
            poolManager.Push(bullet);
            _isFiring = false;
        }

        protected void BulletSpawn()
        {
            OnWeaponUseEvent?.Invoke();
            Bullet bullet = poolManager.Pop<Bullet>(bulletPoolData);
            bullet.transform.position = firePos.position;
            bullet.Shoot(transform.right, _targetPos);
            StartCoroutine(FireCoroutine(bullet));
        }

        public override void WeaponEquip()
        {
            StartCoroutine(CanUseGunCoroutine());
        }

        protected IEnumerator CanUseGunCoroutine()
        {
            _canUseGun = false;
            yield return _waitForGunSettingSeconds;
            _canUseGun =  true;
        }
    }
}
