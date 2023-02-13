using System;
using CodeBase.GameLogic.Bullets;
using CodeBase.Infrastructure.Configs;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CodeBase.GameLogic.Asteroid
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : MonoBehaviour, IAsteroid, IPoolable<Vector2, float, AsteroidConfig, IMemoryPool>, IDisposable
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public event Action<Asteroid, AsteroidSize, Vector2> Dead;

        private AsteroidSize _asteroidSize;
        private IMemoryPool _pool;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IBullet _))
                Dead?.Invoke(this, _asteroidSize, transform.position);
        }
        
        public void OnSpawned(Vector2 position, float angle, AsteroidConfig config, IMemoryPool pool)
        {
            ResetManually(position, angle, config);

            _pool = pool;
        }

        public void OnDespawned() =>
            _pool = null;

        public void Dispose() =>
            _pool.Despawn(this);
        
        private void ResetManually(Vector2 position, float angle, AsteroidConfig config)
        {
            _asteroidSize = config.Size;

            transform.rotation = Quaternion.Euler(0f, 0f, angle + Random.Range(0f, 360f));
            transform.localScale = Vector2.one * config.Scale;
            
            _rigidbody.position = position;
            _rigidbody.AddForce(transform.up * config.Speed, ForceMode2D.Impulse);

            _spriteRenderer.sprite = config.GetRandomSprite();
        }

        public class Factory : PlaceholderFactory<Vector2, float, AsteroidConfig, Asteroid>
        {
        }
    }
}