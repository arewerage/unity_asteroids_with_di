using UnityEngine;
using Zenject;

namespace CodeBase.GameLogic.Bullet
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour, IBullet
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        private void ApplyImpulse(float speed, float angle)
        {
            _rigidbody.SetRotation(angle);
            _rigidbody.AddForce(transform.up * speed, ForceMode2D.Impulse);
        }

        public class Pool : MonoMemoryPool<float, float, Bullet>
        {
            protected override void Reinitialize(float speed, float angle, Bullet bullet)
            {
                bullet.ApplyImpulse(speed, angle);
            }
        }
    }

    public interface IBullet
    {
    }
}
