using System;
using System.Collections.Generic;

namespace CodeBase.GameLogic.Asteroid
{
    public interface IAsteroidsSpawner
    {
        event Action WaveCompleted;
        List<Asteroid> ActiveAsteroids { get; }
        void Spawn(int counts);
        void DespawnAll();
    }
}