using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.GameLogic.Bullets
{
    public class BulletsSpawner : IBulletsSpawner
    {
        private const float DefaultBulletSpeed = 10f;
        
        private readonly Bullet.Factory _bulletFactory;
        private readonly List<Bullet> _bullets = new List<Bullet>();

        public BulletsSpawner(Bullet.Factory bulletFactory)
        {
            _bulletFactory = bulletFactory;
        }

        public void Spawn(Transform parent, float speed = DefaultBulletSpeed)
        {
            Bullet bullet = _bulletFactory.Create(parent, speed);
            
            bullet.Dead += OnDead;
            
            _bullets.Add(bullet);
        }

        public void DespawnAll()
        {
            for (int i = 0; i < _bullets.Count; i++)
                Despawn(_bullets[i]);
        }
        
        private void OnDead(Bullet bullet) =>
            Despawn(bullet);

        private void Despawn(Bullet bullet)
        {
            _bullets.Remove(bullet);
            bullet.Dead -= OnDead;
            bullet.Dispose();
        }
    }
}