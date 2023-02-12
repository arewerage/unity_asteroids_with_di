using System;
using CodeBase.GameLogic.Ship;
using CodeBase.Infrastructure.Configs;
using UnityEngine;
using Zenject;

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
            if (other.gameObject.TryGetComponent(out IShip _))
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

            _rigidbody.position = position;
            _rigidbody.rotation = angle;
            transform.localScale = Vector2.one * config.Scale;

            _spriteRenderer.sprite = config.GetRandomSprite();
        }

        public class Factory : PlaceholderFactory<Vector2, float, AsteroidConfig, Asteroid>
        {
        }
    }
}