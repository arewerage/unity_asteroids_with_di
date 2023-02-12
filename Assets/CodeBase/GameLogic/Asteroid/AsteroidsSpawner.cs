using System.Collections.Generic;
using CodeBase.Infrastructure.Configs;
using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.GameLogic.Asteroid
{
    public class AsteroidsSpawner : IAsteroidsSpawner
    {
        private readonly Asteroid.Factory _asteroidFactory;
        private readonly IAsteroidConfigService _asteroidConfigService;
        private readonly List<Asteroid> _asteroids = new List<Asteroid>();
        
        public AsteroidsSpawner(Asteroid.Factory asteroidFactory, IAsteroidConfigService asteroidConfigService)
        {
            _asteroidFactory = asteroidFactory;
            _asteroidConfigService = asteroidConfigService;
        }

        public void Spawn(int counts)
        {
            // TODO: Add PlacementService
            // for (int i = 0; i < counts; i++)
            // {
                 AsteroidConfig config = _asteroidConfigService.GetBy(EnumUtils.GetRandomEnumValue<AsteroidSize>());
            //     Spawn(Vector2.one * 5f, 0f, config);
            // }
            
            Spawn(Vector2.one * 5f, 0f, config);
        }
        
        private void Spawn(Vector2 position, float angle, AsteroidConfig config)
        {
            Asteroid asteroid = _asteroidFactory.Create(position, angle, config);
            
            asteroid.Dead += OnDead;
            
            _asteroids.Add(asteroid);
        }

        public void DespawnAll()
        {
            for (int i = 0; i < _asteroids.Count; i++)
                Despawn(_asteroids[i]);
        }
        
        private void OnDead(Asteroid asteroid, AsteroidSize asteroidSize, Vector2 deathPosition)
        {
            Despawn(asteroid);

            if (asteroidSize.TryGetNext(out AsteroidSize size) == false)
                return;
            
            Debug.Log("SPAWN!!!");

            AsteroidConfig config = _asteroidConfigService.GetBy(size);
            
            float angle = 360f / config.InstancesOnDead;

            for (int i = 0; i < config.InstancesOnDead; i++)
                Spawn(deathPosition, i * angle, config);
        }
        
        private void Despawn(Asteroid asteroid)
        {
            _asteroids.Remove(asteroid);
            asteroid.Dead -= OnDead;
            asteroid.Dispose();
        }
    }
}