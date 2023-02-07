using System;
using CodeBase.Configs;
using CodeBase.GameLogic.Spaceship;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CodeBase.GameLogic.Asteroid
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : MonoBehaviour, IAsteroid
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public event Action<Asteroid, AsteroidSize, Vector2> Dead;

        private AsteroidSize _asteroidSize;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IShip _))
                Dead?.Invoke(this, _asteroidSize, transform.position);
        }

        private void ResetManually(AsteroidParams asteroidParams, AsteroidSize size, Sprite sprite, Vector2 position, float angle)
        {
            Transform asteroidTransform = transform;
            
            asteroidTransform.SetPositionAndRotation(position, Quaternion.Euler(0f, 0f, angle));
            asteroidTransform.localScale = Vector2.one * asteroidParams.Scale;
            
            _asteroidSize = size;
            
            _spriteRenderer.sprite = sprite;
            _spriteRenderer.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        }

        public class Pool : MonoMemoryPool<AsteroidParams, AsteroidSize, Sprite, Vector2, float, Asteroid>
        {
            protected override void Reinitialize(AsteroidParams asteroidParams, AsteroidSize size, Sprite sprite, Vector2 position, float angle, Asteroid asteroid)
            {
                asteroid.ResetManually(asteroidParams, size, sprite, position, angle);
            }
        }
    }
}