using System;
using System.Threading.Tasks;
using _06.GameLib.ObjectPool.Runtime;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace _06.GameLib.SoundSystem
{
    public class SoundPlayer : MonoBehaviour, IPoolable
    {
        [SerializeField] private AudioMixerGroup sfxGroup;
        [SerializeField] private AudioMixerGroup musicGroup;
        
        private AudioSource _audioSource;
        public GameObject GameObject => this == null ? null : gameObject;
        public PoolItemSO PoolItem { get; set; }

        public event Action<SoundPlayer> OnSoundFinished;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlaySound(SoundClipSO clipData)
        {
            if (clipData.audioTypes == AudioTypes.Sfx)
            {
                _audioSource.outputAudioMixerGroup = sfxGroup; 
            }
            else if  (clipData.audioTypes == AudioTypes.Music)
            {
                _audioSource.outputAudioMixerGroup = musicGroup;
            }
            
            _audioSource.volume = clipData.volume;
            _audioSource.pitch = clipData.pitch;

            if (clipData.randomizePitch)
            {
                _audioSource.pitch += 
                    Random.Range(-clipData.randomPitchModifier,clipData.randomPitchModifier);
            }

            _audioSource.clip = clipData.audioClip;
            _audioSource.loop = clipData.loop;

            if (!_audioSource.loop)
            {
                float time = _audioSource.clip.length + 2f;
                _ = DisableSoundTimer(time);
            }
            _audioSource.Play();
        }

        private async Task DisableSoundTimer(float time)
        {
            await Awaitable.WaitForSecondsAsync(time);
            OnSoundFinished?.Invoke(this);
        }

        public void ForceStopSound()
        {
            _audioSource.Stop();
        }

        public void ResetItem()
        {
            //ninini
        }
    }
}