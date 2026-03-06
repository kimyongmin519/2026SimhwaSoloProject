using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons
{
    public class Mags : MonoBehaviour, IGunParts
    {
        [field:SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        [SerializeField] private float minLifeTime;
        [SerializeField] private float maxLifeTime;

        public void InitializePart(Sprite sprite)
        {
            SpriteRenderer.sprite = sprite;
            StartCoroutine(LifeTimeCoroutine());
        }

        private IEnumerator LifeTimeCoroutine()
        {
            float lifeTime = Random.Range(minLifeTime, maxLifeTime);
            
            yield return new WaitForSeconds(lifeTime);
            Destroy(gameObject);
        }
    }
}