using UnityEngine;
using Zenject;

namespace CodeBase.GameLogic.Asteroid
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Asteroid : MonoBehaviour, IAsteroid
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public class Pool : MonoMemoryPool<Asteroid>
        {
        }
    }
}