using System;
using Systems.AnimationSystem;
using UnityEngine;

namespace Weapon.Guns
{
    public class Pistol : Gun
    {
        [SerializeField] private AnimParamSO testParam;

        public override void WeaponUse()
        {
            _renderer.PlayClip(testParam.HashValue);
        }

        public override void WeaponServeSkill()
        {
            
        }

        public override void WeaponCancel()
        {
            
        }
    }
}