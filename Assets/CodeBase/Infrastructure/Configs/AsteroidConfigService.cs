using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Configs
{
    public class AsteroidConfigService : IAsteroidConfigService, IInitializable
    {
        private const string ConfigPath = "Configs/Asteroids";
        
        private Dictionary<AsteroidSize, AsteroidConfig> _configs;
        
        public void Initialize() =>
            LoadAll(ConfigPath);

        public void LoadAll(string path) => 
            _configs = Resources.LoadAll<AsteroidConfig>(path)
                .ToDictionary(x => x.Size, x => x);

        public AsteroidConfig GetBy(AsteroidSize size) =>
            _configs.TryGetValue(size, out AsteroidConfig config) ? config : null;
    }
}