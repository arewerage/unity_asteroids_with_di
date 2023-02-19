using System;
using CodeBase.Infrastructure.Configs;
using UnityEngine;
using Zenject;

namespace CodeBase.GameLogic.Asteroid
{
    public class AsteroidsController : IAsteroidsController, IInitializable, IFixedTickable, IDisposable
    {
        private readonly IAsteroidsSpawner _asteroidsSpawner;
        private readonly Camera _gameCamera;
        private readonly GameConfig _gameConfig;

        private int _currentWave;

        public AsteroidsController(IAsteroidsSpawner asteroidsSpawner, Camera gameCamera, GameConfig gameConfig)
        {
            _asteroidsSpawner = asteroidsSpawner;
            _gameCamera = gameCamera;
            _gameConfig = gameConfig;
        }

        public void Spawn()
        {
            SpawnWave(_gameConfig.StartingAsteroidsCount);
            _currentWave = 1;
        }

        private void SpawnWave(int count) =>
            _asteroidsSpawner.Spawn(count);

        public void DespawnAll() =>
            _asteroidsSpawner.DespawnAll();

        public void Initialize() =>
            _asteroidsSpawner.WaveCompleted += OnWaveCompleted;

        public void FixedTick()
        {
            foreach (Asteroid asteroid in _asteroidsSpawner.ActiveAsteroids)
                asteroid.WrapScreen(_gameCamera);
        }
        
        public void Dispose() =>
            _asteroidsSpawner.WaveCompleted -= OnWaveCompleted;
        
        private void OnWaveCompleted()
        {
            SpawnWave(_gameConfig.StartingAsteroidsCount + _currentWave);
            _currentWave++;
            
            Debug.Log("Next Wave!");
        }
    }
}
