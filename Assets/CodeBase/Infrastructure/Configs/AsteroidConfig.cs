using UnityEngine;

namespace CodeBase.Infrastructure.Configs
{
    [CreateAssetMenu(menuName = "Configs/Asteroid", fileName = "AsteroidConfig")]
    public class AsteroidConfig : ScriptableObject
    {
        [SerializeField] private Sprite[] _sprites;
        
        public AsteroidSize Size;
        [Range(0.1f, 1f)] public float Scale;
        [Range(1f, 3f)] public float Speed;
        [Range(0f, 5f)] public float InstancesOnDead;
        
        public Sprite GetRandomSprite() =>
            _sprites[Random.Range(0, _sprites.Length)];
    }
}
