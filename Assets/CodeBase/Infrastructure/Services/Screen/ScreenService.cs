using UnityEngine;

namespace CodeBase.Infrastructure.Services.Screen
{
    public class ScreenService : IScreenService
    {
        private readonly Camera _camera;
    
        public ScreenService(Camera camera) =>
            _camera = camera;

        public Rect GetScreenSize()
        {
            Vector2 origin = _camera.transform.position;
            origin.x -= GetHalfWidth();
            origin.y -= _camera.orthographicSize;
            
            return new Rect(origin, new Vector2(GetHalfWidth() * 2f, _camera.orthographicSize * 2f));
        }

        private float GetHalfWidth() =>
            _camera.aspect * _camera.orthographicSize;
    }
}