using System.Collections;
using _06.GameLib.ObjectPool.Runtime;
using _06.GameLib.Runtime;
using Systems.CombatSystem.EffectSystem;
using Systems.GameEvents;
using UnityEngine;

namespace Systems.Manager
{
    public class EffectCreateManager : MonoBehaviour
    {
        [field:SerializeField] public EventChannelSO CreateChannel { get; private set; }
        
        [SerializeField] private GameObject effectPrefab;
        [SerializeField] private PoolItemSO effectPoolItem;
        [SerializeField] private PoolManagerSO poolManager;

        private void Awake()
        {
            CreateChannel.AddListener<CreateEffectEvent>(HandleCreateEffect);
        }

        private void OnDestroy()
        {
            CreateChannel.RemoveListener<CreateEffectEvent>(HandleCreateEffect);;
        }

        private void HandleCreateEffect(CreateEffectEvent evt)
        {
            if (evt.IsPoolingEffect)
            {
                PoolAnimateEffect effect = poolManager.Pop<PoolAnimateEffect>(effectPoolItem);
                effect.PlayEffectClip(evt.Position, evt.Rotation, evt.ClipHash);
                StartCoroutine(HandleLifeTimeEnd(effect, evt.Duration)); 
            }
            else
            {
                GameObject effectObject = Instantiate(effectPrefab, evt.Position, evt.Rotation);
                AnimatorEffect effect = effectObject.GetComponent<AnimatorEffect>();
                
                effect.PlayerEffectClip(evt.ClipHash, evt.Duration);
            }
        }

        private IEnumerator HandleLifeTimeEnd(PoolAnimateEffect effect, float evtDuration)
        {
            yield return new WaitForSeconds(evtDuration);
            poolManager.Push(effect);
        }
    }
}