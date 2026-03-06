using Systems.CombatSystem;
using UnityEngine;

namespace Weapons.Guns
{
    [CreateAssetMenu(fileName = "Gun data", menuName = "Weapon/Gun data", order = 15)]
    public class GunDataSO : WeaponDataSO
    {
        [field:SerializeField, Header("수치값")]
        public int Ammo {get; private set;}
        [field:SerializeField] public int MaxAmmo {get; private set;}
        [field:SerializeField] public float Damage {get; private set;}
        [field:SerializeField] public float HeadShotDamage {get; private set;}

        [field: SerializeField,Header("여부값")]
        public bool CanAutoFire {get; private set;}

        [field: SerializeField,Header("총 구성값")]
        public AnimationClip EquipAnimClip { get; private set; }
        [field: SerializeField] public AnimationClip IdleAnimClip { get; private set; }
        [field:SerializeField] public AnimationClip ShotAnimClip {get; private set;}
        [field:SerializeField] public AnimationClip ReloadAnimClip {get; private set;}
        [field:SerializeField] public Bullet BulletPrefab {get; private set;}
        [field:SerializeField] public Sprite MagsSprite {get; private set;}
    }
}