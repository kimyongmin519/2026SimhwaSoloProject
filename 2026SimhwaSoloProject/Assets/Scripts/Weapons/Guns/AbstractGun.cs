using _06.GameLib.ObjectPool.Runtime;
using Core;
using Systems.CombatSystem;
using Systems.GameEvents.BusEvent;
using Systems.GameEvents.BusEvent.BusEvents.GunEvents;
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
        private int _maxAmmo;

        protected float _fireRate;

        protected bool _isFiring = false;
        protected bool _isReloading = false;

        protected override void Awake()
        {
            base.Awake();
            _gunData = WeaponData as GunDataSO;
            Debug.Assert(Renderer != null, "Gun Renderer is null");
            Debug.Assert(_gunData != null, "Weapon data not casting Gun data");

            _maxAmmo = _gunData.MaxAmmo;
            _currentAmmo = new NotifyValue<int>(_gunData.Ammo);
            _fireRate = _gunData.ShotAnimClip.length;

            KimBus<MagsDropEvent>.OnEvent += MagsDrop;
            KimBus<AmmoReloadEvent>.OnEvent += AmmoReload;
            
            _waitForSecWeapon = new WaitForSeconds(_gunData.EquipAnimClip.length);
        }

        protected bool BulletSpawn()
        {
            if (_currentAmmo.Value <= 0)
            {
                return false;
            }
            else
            {
                OnWeaponUseEvent?.Invoke();
                Bullet bullet = poolManager.Pop<Bullet>(bulletPoolData);
                bullet.transform.position = firePos.position;
                bullet.Shoot(_gunData.Damage ,transform.right, _targetPos);

                _currentAmmo.Value -= 1;

                return true;
            }
        }

        public virtual void GunReload()
        {
            OnGunReload?.Invoke();
        }
        private void MagsDrop(MagsDropEvent evt)
        {
            Mags mags = Instantiate(magPrefab, magsPos.position, Quaternion.identity);
            mags.InitializePart(_gunData.MagsSprite);
        }

        private void AmmoReload(AmmoReloadEvent evt)
        {
            _maxAmmo += _currentAmmo.Value;
            _maxAmmo -= _gunData.Ammo;
            
            _currentAmmo.Value = _gunData.Ammo;
        }

        private void OnDestroy()
        {
            KimBus<MagsDropEvent>.OnEvent -= MagsDrop;
            KimBus<AmmoReloadEvent>.OnEvent -= AmmoReload;
        }
    }
}
