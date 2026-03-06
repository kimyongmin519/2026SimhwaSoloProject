using UnityEngine;

namespace Weapons.Guns
{
    public class FirePos : MonoBehaviour
    {
        [SerializeField] private ParticleSystem fireParticle;

        public void PlayEffect()
        {
            fireParticle.Play();
        }
    }
}
