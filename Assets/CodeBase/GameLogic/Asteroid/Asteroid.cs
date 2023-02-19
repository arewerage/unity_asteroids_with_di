using System;
using CodeBase.GameLogic.Bullets;
using CodeBase.GameLogic.Ship;
using CodeBase.Infrastructure.Configs.Asteroids;
using CodeBase.Utils;
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
        private Transform _transform;
        private AsteroidData _asteroidData;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IBullet _)
                || other.gameObject.TryGetComponent(out IShip _))
                Dead?.Invoke(this, _asteroidData, _transform.position);
        }
        
        public void OnSpawned(Vector2 position, float angle, Sprite sprite, AsteroidData data, IMemoryPool pool)
        {
            _transform ??= transform;
            
            ResetManually(position, angle, sprite, data);

            _pool = pool;
        }

        public void OnDespawned() =>
            _pool = null;

        public void Dispose() =>
            _pool.Despawn(this);
        
        public void WrapScreen(Camera gameCamera, float offset = 0.1f)
        {
            Vector2 position = _transform.position;
            Vector2 ndcPosition = gameCamera.WorldToNdc(position);
        
            if (Mathf.Abs(ndcPosition.x) - offset > 1f)
            {
                position.x = -position.x;
                position.x += ndcPosition.x * offset;
            }
        
            if (Mathf.Abs(ndcPosition.y) - offset > 1f)
            {
                position.y = -position.y;
                position.y += ndcPosition.y * offset;
            }
        
            _transform.position = position;
        }
        
        private void ResetManually(Vector2 position, float angle, Sprite sprite, AsteroidData data)
        {
            _asteroidData = data;

            _transform.SetPositionAndRotation(position, Quaternion.Euler(0f, 0f, angle + Random.Range(0f, 360f)));
            _transform.localScale = Vector2.one * data.Scale;
            
            _rigidbody.AddForce(_transform.up * data.SpeedFactor, ForceMode2D.Impulse);

            _spriteRenderer.sprite = sprite;
        }

        public class Factory : PlaceholderFactory<Vector2, float, Sprite, AsteroidData, Asteroid>
        {
        }
    }
}