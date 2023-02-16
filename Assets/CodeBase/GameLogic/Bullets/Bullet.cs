using System;
using CodeBase.GameLogic.Asteroid;
using UniRx;
using UnityEngine;
using Zenject;

namespace CodeBase.GameLogic.Bullets
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour, IBullet
    {
        private const double LifeTime = 2;
        
        [SerializeField] private Rigidbody2D _rigidbody;
        
        public event Action<Bullet> Dead;

        private IDisposable _timer;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IObstacle _))
                Dead?.Invoke(this);
        }

        private void OnDisable() =>
            _timer?.Dispose();

        private void ResetManually(Transform parent, float speed)
        {
            Transform cachedTransform = transform;
            
            cachedTransform.SetPositionAndRotation(parent.position, parent.rotation);
            _rigidbody.AddForce(cachedTransform.up * speed, ForceMode2D.Impulse);
            
            SetLifeTimer();
        }

        private void SetLifeTimer()
        {
            _timer = Observable.Timer(TimeSpan.FromSeconds(LifeTime)).Subscribe(_ =>
            {
                Dead?.Invoke(this);
            });
        }

        public class Pool : MonoMemoryPool<Transform, float, Bullet>
        {
            protected override void Reinitialize(Transform parent, float speed, Bullet bullet) =>
                bullet.ResetManually(parent, speed);
        }
    }
}