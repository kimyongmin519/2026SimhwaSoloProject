using UnityEngine;

namespace Weapons.Melees
{
    [CreateAssetMenu(fileName = "Melee weapon data", menuName = "Weapon/Melee weapon data", order = 15)]
    public class MeleeWeaponDataSO : WeaponDataSO
    {
        [field:SerializeField,Header("근접무기 세팅값")]
        public float Damage {get; private set;}
        
        [field:SerializeField,Header("근접무기 애니메이션")]
        public AnimationClip EquipAnimClip { get; private set; }
        [field: SerializeField] public AnimationClip IdleAnimClip { get; private set; }
        [field: SerializeField] public AnimationClip AttackAnimClip { get; private set; }
    }
}