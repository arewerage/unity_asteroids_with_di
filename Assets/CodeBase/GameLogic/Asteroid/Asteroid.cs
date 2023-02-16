using System;
using CodeBase.GameLogic.Bullets;
using CodeBase.GameLogic.Ship;
using CodeBase.Infrastructure.Configs.Asteroids;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CodeBase.GameLogic.Asteroid
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : MonoBehaviour, IAsteroid, IPoolable<Vector2, float, Sprite, AsteroidData, IMemoryPool>, IDisposable
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public event Action<Asteroid, AsteroidData, Vector2> Dead;

        private IMemoryPool _pool;
        private AsteroidData _asteroidData;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IBullet _)
                || other.gameObject.TryGetComponent(out IShip _))
                Dead?.Invoke(this, _asteroidData, transform.position);
        }
        
        public void OnSpawned(Vector2 position, float angle, Sprite sprite, AsteroidData data, IMemoryPool pool)
        {
            ResetManually(position, angle, sprite, data);

            _pool = pool;
        }

        public void OnDespawned() =>
            _pool = null;

        public void Dispose() =>
            _pool.Despawn(this);
        
        private void ResetManually(Vector2 position, float angle, Sprite sprite, AsteroidData data)
        {
            Transform cachedTransform = transform;
            
            _asteroidData = data;

            cachedTransform.rotation = Quaternion.Euler(0f, 0f, angle + Random.Range(0f, 360f));
            cachedTransform.localScale = Vector2.one * data.Scale;
            
            _rigidbody.position = position;
            _rigidbody.AddForce(cachedTransform.up * data.SpeedFactor, ForceMode2D.Impulse);

            _spriteRenderer.sprite = sprite;
        }

        public class Factory : PlaceholderFactory<Vector2, float, Sprite, AsteroidData, Asteroid>
        {
        }
    }
}