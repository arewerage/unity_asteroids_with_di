using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Configs.Asteroids
{
    [Serializable]
    public class AsteroidData
    {
        public AsteroidSize Size;
        [Range(0.1f, 1f)] public float Scale;
        [Range(0.6f, 3f)] public float SpeedFactor;
        [Range(0f, 4f)] public int Childs;
    }
}