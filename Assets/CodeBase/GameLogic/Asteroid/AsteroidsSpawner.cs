using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Configs.Asteroids;
using CodeBase.Infrastructure.Services.ObstaclePlacement;
using CodeBase.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.GameLogic.Asteroid
{
    public class AsteroidsSpawner : IAsteroidsSpawner
    {
        private readonly Asteroid.Factory _asteroidFactory;
        private readonly AsteroidsConfig _asteroidsConfig;
        private readonly IObstaclePlacementService _obstaclePlacementService;
        private readonly List<Asteroid> _activeAsteroids = new List<Asteroid>();

        public event Action WaveCompleted;
        
        public List<Asteroid> ActiveAsteroids => _activeAsteroids;
        
        public AsteroidsSpawner(Asteroid.Factory asteroidFactory,
            AsteroidsConfig asteroidsConfig,
            IObstaclePlacementService obstaclePlacementService)
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

            if (TrySpawnChild(asteroidData, deathPosition))
                return;

            if (IsAsteroidsCountEqualZero())
                WaveCompleted?.Invoke();
        }
        
        private void Despawn(Asteroid asteroid)
        {
            asteroid.Dead -= OnDead;
            asteroid.Dispose();
            _activeAsteroids.Remove(asteroid);
        }

        private bool TrySpawnChild(AsteroidData data, Vector2 position)
        {
            if (data.Size.TryGetNext(out AsteroidSize nextSize) == false)
                return false;

            AsteroidData nextAsteroidData = _asteroidsConfig.Get(nextSize);
            float angle = 360f / data.Childs;

            for (int i = 0; i < data.Childs; i++)
                Spawn(position, i * angle, nextAsteroidData);

            return true;
        }

        private bool IsAsteroidsCountEqualZero() =>
            _activeAsteroids.Count == 0;
    }
}