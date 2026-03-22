using UnityEngine;

namespace Weapons.Guns
{
    public class GunRenderer : WeaponRenderer
    {
        private AbstractGun _ownerGun;
        
        protected override void Awake()
        {
            base.Awake();
            _ownerGun = GetComponent<AbstractGun>();
            Debug.Assert(_ownerGun != null, "_ownerGun != null");
        }
        public void DropMags()
        {
            _ownerGun.MagsDrop();
        }

        public void ReloadAmmo()
        {
            _ownerGun.AmmoReload();
        }

    }
}