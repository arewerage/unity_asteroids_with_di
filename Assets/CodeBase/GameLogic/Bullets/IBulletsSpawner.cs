using UnityEngine;

namespace CodeBase.GameLogic.Bullets
{
    public interface IBulletsSpawner
    {
        void Spawn(Transform parent, float speed = 10f);
        void DespawnAll();
    }
}
