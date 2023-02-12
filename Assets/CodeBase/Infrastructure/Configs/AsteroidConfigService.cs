using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Infrastructure.Configs
{
    public class AsteroidConfigService : IAsteroidConfigService
    {
        private const string ConfigPath = "Configs/Asteroids";
        
        private Dictionary<AsteroidSize, AsteroidConfig> _configs;

        public void LoadAll() => 
            _configs = Resources.LoadAll<AsteroidConfig>(ConfigPath)
                .ToDictionary(x => x.Size, x => x);

        public AsteroidConfig GetBy(AsteroidSize size) =>
            _configs.TryGetValue(size, out AsteroidConfig config) ? config : null;
    }
}