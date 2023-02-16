using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.GameLogic.Bullets
{
    public class BulletsSpawner : IBulletsSpawner
    {
        private const float DefaultBulletSpeed = 10f;
        
        private readonly Bullet.Pool _bulletPool;
        private readonly List<Bullet> _activeBullets = new List<Bullet>();

        public BulletsSpawner(Bullet.Pool bulletPool) =>
            _bulletPool = bulletPool;

        public void Spawn(Transform parent, float speed = DefaultBulletSpeed)
        {
            Bullet bullet = _bulletPool.Spawn(parent, speed);
            
            bullet.Dead += OnDead;
            
            _activeBullets.Add(bullet);
        }

        public void DespawnAll()
        {
            foreach (Bullet bullet in _activeBullets.ToArray())
                Despawn(bullet);
        }
        
        private void OnDead(Bullet bullet) =>
            Despawn(bullet);

        private void Despawn(Bullet bullet)
        {
            bullet.Dead -= OnDead;
            _bulletPool.Despawn(bullet);
            _activeBullets.Remove(bullet);
        }
    }
}