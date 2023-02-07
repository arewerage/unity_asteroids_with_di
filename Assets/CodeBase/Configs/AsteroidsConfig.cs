using System;
using CodeBase.GameLogic.Asteroid;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Configs
{
    [CreateAssetMenu(menuName = "Configs/Asteroids", fileName = "AsteroidsConfig")]
    public class AsteroidsConfig : ScriptableObject
    {
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private AsteroidParams _big;
        [SerializeField] private AsteroidParams _medium;
        [SerializeField] private AsteroidParams _small;

        public Sprite RandomSprite() =>
            _sprites[Random.Range(0, _sprites.Length)];

        public AsteroidParams GetConfig(AsteroidSize size)
        {
            return size switch
            {
                AsteroidSize.Small => _small,
                AsteroidSize.Medium => _medium,
                AsteroidSize.Big => _big,
                _ => throw new ArgumentOutOfRangeException(nameof(size), size, null)
            };
        }
    }

    [Serializable]
    public class AsteroidParams
    {
        [Range(0.1f, 1f)] public float Scale;
        [Range(1f, 3f)] public float Speed;
        [Range(0f, 5f)] public float Childs;
    }
}