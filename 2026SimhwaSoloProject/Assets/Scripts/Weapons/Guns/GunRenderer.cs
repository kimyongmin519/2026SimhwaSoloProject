using Systems.GameEvents.BusEvent;
using Systems.GameEvents.BusEvent.BusEvents.GunEvents;

namespace Weapons.Guns
{
    public class GunRenderer : WeaponRenderer
    {
        public void DropMags()
        {
            KimBus<MagsDropEvent>.RaiseEvent(new MagsDropEvent());
        }

        public void ReloadAmmo()
        {
            KimBus<AmmoReloadEvent>.RaiseEvent(new AmmoReloadEvent());
        }
    }
}