using System.Collections;
using _06.GameLib.ObjectPool.Runtime;
using Systems.CombatSystem.EffectSystem;
using Systems.GameEvents;
using Systems.GameEvents.ChannelEvent;
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
            CreateChannel.AddListener<CreateObjectEvent>(HandleCreateParticle);
        }

        private void OnDestroy()
        {
            CreateChannel.RemoveListener<CreateEffectEvent>(HandleCreateEffect);
            CreateChannel.RemoveListener<CreateObjectEvent>(HandleCreateParticle);
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
        
        private void HandleCreateParticle(CreateObjectEvent evt)
        {
            if (evt.IsPoolingEffect)
            {
                PoolMono effect = poolManager.Pop<PoolMono>(evt.PoolItem);
                effect.transform.position = evt.Position;
                effect.transform.rotation = evt.Rotation;
                StartCoroutine(HandleLifeTimeEnd(effect, evt.Duration));
            }
            else
            {
                GameObject effectObject = Instantiate(effectPrefab, evt.Position, evt.Rotation);
                PoolMono effect = effectObject.GetComponent<PoolMono>();
            }
        }

        private IEnumerator HandleLifeTimeEnd(PoolAnimateEffect effect, float evtDuration)
        {
            yield return new WaitForSeconds(evtDuration);
            poolManager.Push(effect);
        }
        
        private IEnumerator HandleLifeTimeEnd(PoolMono effect, float evtDuration)
        {
            yield return new WaitForSeconds(evtDuration);
            poolManager.Push(effect);
        }
    }
}