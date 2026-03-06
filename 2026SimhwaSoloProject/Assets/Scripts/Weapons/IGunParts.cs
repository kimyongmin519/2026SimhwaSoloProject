using UnityEngine;

namespace Weapons
{
    public interface IGunParts
    {
        public SpriteRenderer SpriteRenderer { get; }
        public void InitializePart(Sprite sprite);
    }
}