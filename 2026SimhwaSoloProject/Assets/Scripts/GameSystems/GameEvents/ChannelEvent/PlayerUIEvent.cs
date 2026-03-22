using _06.GameLib.EventSystem;
using Weapons;

namespace GameSystems.GameEvents.ChannelEvent
{
    public class PlayerUIEvent
    {
        public static PlayerInventoryChange PlayerInventoryChange = new PlayerInventoryChange();
        public static GunValueChanged GunValueChanged = new GunValueChanged();
        public static CurrentGunReload CurrentGunReload = new CurrentGunReload();
    }

    public class PlayerInventoryChange : GameEvent
    {
        public Weapon Weapon { get; private set; }

        public PlayerInventoryChange Init(Weapon getWeapon)
        {
            Weapon = getWeapon;
            
            return this;
        }
    }

    public class GunValueChanged : GameEvent
    {
        public int CurrentAmmo { get; private set; }
        public int LeftAmmo {get; private set;}

        public GunValueChanged Init(int currentAmmo, int leftAmmo)
        {
            CurrentAmmo = currentAmmo;
            LeftAmmo = leftAmmo;

            return this;
        }
    }

    public class CurrentGunReload : GameEvent {}
}