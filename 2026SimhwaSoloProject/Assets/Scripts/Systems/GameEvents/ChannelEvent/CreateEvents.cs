using _06.GameLib.ObjectPool.Runtime;
using UnityEngine;

namespace Systems.GameEvents.ChannelEvent
{
    public static class CreateEvents
    {
        public static readonly CreateEffectEvent CreateEffect = new CreateEffectEvent();
        public static readonly CreateObjectEvent CreateParticle = new CreateObjectEvent();
    }
    
    public class CreateObjectEvent : GameEvent
    {
        public Vector3 Position { get; private set; }
        public Quaternion Rotation { get; private set; }
        public float Duration {get; private set;}
        public PoolItemSO PoolItem { get; private set; }
        
        public bool IsPoolingEffect {get; private set;}
        
        public CreateObjectEvent Init(Vector3 position, Quaternion rotation, PoolItemSO poolItem
            ,bool isPoolingEffect = false ,float duration = 1f)
        {
            Position = position;
            Rotation = rotation;
            PoolItem = poolItem;
            IsPoolingEffect = isPoolingEffect;
            Duration = duration;
            return this;
        }
    }
    public class CreateEffectEvent : GameEvent
    {
        public Vector3 Position { get; private set; }
        public Quaternion Rotation { get; private set; }
        public int ClipHash { get; private set; }
        public float Duration { get; private set; }
        public bool IsPoolingEffect {get; private set;}

        public CreateEffectEvent Init(Vector3 position, Quaternion rotation, int clipHash
            , bool isPoolingEffect = false, float duration = 1f)
        {
            Position = position;
            Rotation = rotation;
            ClipHash = clipHash;
            Duration = duration;
            IsPoolingEffect = isPoolingEffect;
            return this;
        }
    }
}