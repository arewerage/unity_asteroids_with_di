using System;
using CodeBase.GameLogic.Asteroid;
using CodeBase.Utils;
using UnityEngine;
using Zenject;

namespace CodeBase.GameLogic.Ship
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ship : MonoBehaviour, IShip
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        
        public event Action Dead;

        private Transform _transform;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IObstacle _))
                Dead?.Invoke();
        }

        public void AddThrust(float thrustSpeed) =>
            _rigidbody.AddForce(_transform.up * thrustSpeed);

        public void Turn(float direction, float turnSpeed) =>
            _rigidbody.AddTorque(-direction * turnSpeed);

        public void WrapScreen(Camera gameCamera, float offset = 0.1f)
        {
            Vector2 position = _transform.position;
            Vector2 ndcPosition = gameCamera.WorldToNdc(position);
        
            if (Mathf.Abs(ndcPosition.x) > 1f)
            {
                position.x = -position.x;
                position.x += ndcPosition.x * offset;
            }
        
            if (Mathf.Abs(ndcPosition.y) > 1f)
            {
                position.y = -position.y;
                position.y += ndcPosition.y * offset;
            }
        
            _transform.position = position;
        }

        private void ResetManually()
        {
            _transform ??= transform;
            _transform.SetPositionAndRotation(Vector2.zero, Quaternion.identity);
        }

        public class Pool : MonoMemoryPool<Ship>
        {
            protected override void Reinitialize(Ship ship) =>
                ship.ResetManually();
        }
    }
}