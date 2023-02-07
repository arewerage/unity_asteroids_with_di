using System.Collections.Generic;
using CodeBase.Configs;
using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.GameLogic.Asteroid
{
    public class AsteroidsFactory
    {
        private readonly Asteroid.Pool _asteroidPool;
        private readonly AsteroidsConfig _asteroidsConfig;

        private List<Asteroid> _asteroids;
        
        public AsteroidsFactory(Asteroid.Pool asteroidPool, AsteroidsConfig asteroidsConfig)
        {
            _asteroidPool = asteroidPool;
            _asteroidsConfig = asteroidsConfig;
        }

        public void Spawn(int count = 0)
        {
            Spawn(Vector2.zero, Random.Range(0f, 360f));
        }

        public void DespawnAll()
        {
            foreach (Asteroid asteroid in _asteroids)
                Despawn(asteroid);
        }
        
        private void Spawn(Vector2 position, float angle, AsteroidParams asteroidParams = null, AsteroidSize asteroidSize = AsteroidSize.Big)
        {
            asteroidParams ??= _asteroidsConfig.GetConfig(asteroidSize);

            Sprite sprite = _asteroidsConfig.GetRandomSprite();
            
            Asteroid asteroid = _asteroidPool.Spawn(asteroidParams, asteroidSize, sprite, position, angle);

            asteroid.Dead += OnDead;
            
            _asteroids.Add(asteroid);
        }
        
        private void OnDead(Asteroid asteroid, AsteroidSize asteroidSize, Vector2 deathPosition)
        {
            asteroid.Dead -= OnDead;
            
            Despawn(asteroid);

            if (!asteroidSize.TryGetNext(out AsteroidSize nextSize))
                return;

            AsteroidParams asteroidParams = _asteroidsConfig.GetConfig(nextSize);
            float angle = 360f / asteroidParams.Childs;

            for (int i = 0; i < asteroidParams.Childs; i++)
                Spawn(deathPosition, i * angle, asteroidParams, nextSize);
        }
        
        private void Despawn(Asteroid asteroid)
        {
            _asteroids.Remove(asteroid);
            _asteroidPool.Despawn(asteroid);
        }
    }
}