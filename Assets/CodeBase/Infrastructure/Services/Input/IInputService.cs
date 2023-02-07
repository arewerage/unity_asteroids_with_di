using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputService
    {
        event Action<Vector2> Moved;
        event Action Fired;
        event Action Paused;
        event Action Played;
        
        void EnableGameplay();
        void EnableUI();
    }
}
