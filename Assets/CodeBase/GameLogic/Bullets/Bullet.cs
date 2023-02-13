using System;
using CodeBase.GameLogic.Asteroid;
using UnityEngine;
using Zenject;

namespace CodeBase.GameLogic.Bullets
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour, IBullet, IPoolable<Transform, float, IMemoryPool>, IDisposable
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        
        public event Action<Bullet> Dead;

        private IMemoryPool _pool;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IObstacle _))
                Dead?.Invoke(this);
        }

        public void OnSpawned(Transform parent, float speed, IMemoryPool pool)
        {
            Transform cachedTransform = transform;
            
            cachedTransform.SetPositionAndRotation(parent.position, parent.rotation);
            _rigidbody.AddForce(cachedTransform.up * speed, ForceMode2D.Impulse);
            
            _pool = pool;
        }
        
        public void OnDespawned() =>
            _pool = null;

        public void Dispose() =>
            _pool.Despawn(this);

        public class Factory : PlaceholderFactory<Transform, float, Bullet>
        {
        }
    }
}