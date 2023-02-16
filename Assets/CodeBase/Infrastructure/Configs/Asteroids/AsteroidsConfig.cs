using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Infrastructure.Configs.Asteroids
{
    [CreateAssetMenu(menuName = "Configs/Asteroids", fileName = "AsteroidsConfig")]
    public class AsteroidsConfig : ScriptableObject
    {
        [SerializeField] private Sprite[] _sprites;
        
        [SerializeField] private AsteroidData _big;
        [SerializeField] private AsteroidData _medium;
        [SerializeField] private AsteroidData _small;

        public AsteroidData Get(AsteroidSize size)
        {
            return size switch
            {
                AsteroidSize.Big => _big,
                AsteroidSize.Medium => _medium,
                AsteroidSize.Small => _small,
                _ => throw new ArgumentOutOfRangeException(nameof(size), size, null)
            };
        }
        
        public Sprite GetRandomSprite() =>
            _sprites[Random.Range(0, _sprites.Length)];
    }
}
