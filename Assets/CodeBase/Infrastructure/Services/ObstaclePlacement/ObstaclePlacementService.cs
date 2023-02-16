using CodeBase.Infrastructure.Services.Screen;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Services.ObstaclePlacement
{
    public class ObstaclePlacementService : IObstaclePlacementService, IInitializable
    {
        private readonly IScreenService _screenService;
        
        private Rect _cachedScreenSize;
        
        public ObstaclePlacementService(IScreenService screenService) =>
            _screenService = screenService;
        
        public void Initialize() =>
            _cachedScreenSize = _screenService.GetScreenSize();

        public Vector2 GetRandomInsideScreenPosition()
        {
            return new Vector2(
                Random.Range(_cachedScreenSize.xMin, _cachedScreenSize.xMax),
                Random.Range(_cachedScreenSize.yMin, _cachedScreenSize.yMax));
        }
    }
}