﻿using System;
using CodeBase.GameLogic.Asteroid;
using UnityEngine;
using Zenject;

namespace CodeBase.GameLogic.Spaceship
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ship : MonoBehaviour, IShip, IPoolable<IMemoryPool>, IDisposable
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        public event Action Dead;

        private IMemoryPool _pool;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IObstacle _))
                Dead?.Invoke();
        }
        
        public void OnSpawned(IMemoryPool pool)
        {
            transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            _pool = pool;
        }

        public void OnDespawned() =>
            _pool = null;

        public void Dispose() =>
            _pool.Despawn(this);

        public void AddThrust(float thrustSpeed) =>
            _rigidbody.AddForce(transform.up * thrustSpeed);

        public void Turn(float direction, float turnSpeed) =>
            _rigidbody.AddTorque(direction * turnSpeed);

        public class Factory : PlaceholderFactory<Ship>
        {
        }
    }
}