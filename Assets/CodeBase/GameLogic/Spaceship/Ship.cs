using System;
using CodeBase.GameLogic.Asteroid;
using UnityEngine;
using Zenject;

namespace CodeBase.GameLogic.Spaceship
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ship : MonoBehaviour, IShip
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        public event Action Dead;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IObstacle _))
                Dead?.Invoke();
        }

        public void AddThrust(float thrustSpeed) =>
            _rigidbody.AddForce(transform.up * thrustSpeed);

        public void Turn(float direction, float turnSpeed) =>
            _rigidbody.AddTorque(direction * turnSpeed);

        public class Pool : MonoMemoryPool<Ship>
        {
            protected override void Reinitialize(Ship ship) =>
                ship.transform.position = Vector3.zero;
        }
    }
}