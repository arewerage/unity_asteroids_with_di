using UnityEngine;

namespace CodeBase.Infrastructure.Configs
{
    [CreateAssetMenu(menuName = "Configs/Game", fileName = "GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Range(1f, 10f)] public int StartingAsteroidsCount;
    }
}
