using Core;
using UnityEngine;

namespace Weapon.Guns
{
    public abstract class Gun : Weapon
    {
        private GunDataSO _gunData;
        protected NotifyValue<int> _currentAmmo;
        private int _oneMagazineAmmo;

        protected override void Awake()
        {
            base.Awake();
            _gunData = WeaponData as GunDataSO;
            Debug.Assert(_renderer != null, "Gun Renderer is null");
            Debug.Assert(_gunData != null, "Weapon data not casting Gun data");
            
            _oneMagazineAmmo = _gunData.Ammo;

            _currentAmmo = new NotifyValue<int>(_oneMagazineAmmo);
            
            GunSetting();
        }

        private void GunSetting()
        {
            Vector3 parentPos = transform.parent.localPosition;
            transform.parent.localPosition = new Vector3(parentPos.x, _gunData.PingHeight, parentPos.z);
        }
    }
}
