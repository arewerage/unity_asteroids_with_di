using System.Collections.Generic;
using CodeBase.Infrastructure.Configs.Asteroids;
using CodeBase.Infrastructure.Services.ObstaclePlacement;
using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.GameLogic.Asteroid
{
    public class AsteroidsSpawner : IAsteroidsSpawner
    {
        private readonly Asteroid.Factory _asteroidFactory;
        private readonly AsteroidsConfig _asteroidsConfig;
        private readonly IObstaclePlacementService _obstaclePlacementService;
        private readonly List<Asteroid> _activeAsteroids = new List<Asteroid>();
        
        public AsteroidsSpawner(Asteroid.Factory asteroidFactory, AsteroidsConfig asteroidsConfig, IObstaclePlacementService obstaclePlacementService)
        {
            _asteroidFactory = asteroidFactory;
            _asteroidsConfig = asteroidsConfig;
            _obstaclePlacementService = obstaclePlacementService;
        }

        public void Spawn(int counts)
        {
            AsteroidData config = _asteroidsConfig.Get(AsteroidSize.Big);
            
            for (int i = 0; i < counts; i++)
                Spawn(_obstaclePlacementService.GetRandomInsideScreenPosition(), Random.Range(0f, 360f), config);
        }
        
        private void Spawn(Vector2 position, float angle, AsteroidData asteroidData)
        {
            Sprite sprite = _asteroidsConfig.GetRandomSprite();
            
            Asteroid asteroid = _asteroidFactory.Create(position, angle, sprite, asteroidData);
            
            asteroid.Dead += OnDead;
            
            _activeAsteroids.Add(asteroid);
        }

        public void DespawnAll()
        {
            foreach (Asteroid asteroid in _activeAsteroids.ToArray())
                Despawn(asteroid);
        }
        
        private void OnDead(Asteroid asteroid, AsteroidData asteroidData, Vector2 deathPosition)
        {
            Despawn(asteroid);

            if (asteroidData.Size.TryGetNext(out AsteroidSize nextSize) == false)
                return;

            AsteroidData nextAsteroidData = _asteroidsConfig.Get(nextSize);
            float angle = 360f / asteroidData.Childs + Random.Range(-30f, 31f);

            for (int i = 0; i < asteroidData.Childs; i++)
                Spawn(deathPosition, i * angle, nextAsteroidData);
        }
        
        private void Despawn(Asteroid asteroid)
        {
            asteroid.Dead -= OnDead;
            asteroid.Dispose();
            _activeAsteroids.Remove(asteroid);
        }
    }
}