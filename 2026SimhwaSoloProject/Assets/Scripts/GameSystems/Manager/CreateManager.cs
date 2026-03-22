using System.Collections;
using _06.GameLib.EventSystem;
using _06.GameLib.ObjectPool.Runtime;
using GameSystems.CombatSystem.EffectSystem;
using GameSystems.GameEvents.ChannelEvent;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace GameSystems.Manager
{
    public class CreateManager : MonoBehaviour
    {
        [field:SerializeField] public EventChannelSO CreateChannel { get; private set; }
        
        [SerializeField] private GameObject effectPrefab;
        [SerializeField] private PoolItemSO effectPoolItem;
        [SerializeField] private PoolManagerSO poolManager;
        [SerializeField] private EventChannelSO playerChannel;
        [SerializeField] private Weapon testGun;

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

        private void Update()
        {
            if (Keyboard.current.fKey.wasPressedThisFrame)
            {
                playerChannel.RaiseEvent(PlayerUIEvent.PlayerInventoryChange.Init(testGun));
            }
        }
    }
}