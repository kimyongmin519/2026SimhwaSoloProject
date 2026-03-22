using _06.GameLib.EventSystem;
using _06.GameLib.SoundSystem;
using UnityEngine;

namespace GameSystems.AnimationSystem
{
    public class AnimationSound : MonoBehaviour
    {
        [field: SerializeField] public EventChannelSO SoundChannel { get; private set; }
        
        public void PlayAnimationSound(SoundClipSO clip)
        {
            SoundChannel.RaiseEvent(SoundEvent.PlaySoundEvent.Init(transform.position, clip));
        }
    }
}
