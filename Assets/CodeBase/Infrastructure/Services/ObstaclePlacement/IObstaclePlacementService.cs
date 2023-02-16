using UnityEngine;

namespace CodeBase.Infrastructure.Services.ObstaclePlacement
{
    public interface IObstaclePlacementService
    {
        Vector2 GetRandomInsideScreenPosition();
    }
}