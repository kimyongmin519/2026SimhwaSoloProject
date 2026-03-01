using _06.GameLib.ObjectPool.Runtime;
using UnityEngine;

namespace Systems.CombatSystem.EffectSystem
{
    public class PoolAnimateEffect : PoolMono
    {
        [SerializeField] private AnimatorEffect animatorEffect;

        public void PlayEffectClip(Vector3 position, Quaternion rotation, int clipHash)
        {
            transform.SetPositionAndRotation(position, rotation);
            animatorEffect.PlayerEffectClip(clipHash, 0);
        }
    }
}