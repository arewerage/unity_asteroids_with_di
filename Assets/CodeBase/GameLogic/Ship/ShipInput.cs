using System;
using CodeBase.Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace CodeBase.GameLogic.Ship
{
    public class ShipInput : IShipInput, IInitializable, IDisposable
    {
        private readonly IInputService _inputService;
        
        public event Action Fired;
        
        public bool IsThrusting { get; private set; }
        public float TurnValue { get; private set; }
        
        public ShipInput(IInputService inputService) =>
            _inputService = inputService;

        public void Initialize()
        {
            _inputService.Moved += OnMoved;
            _inputService.Fired += OnFired;
        }

        public void Dispose()
        {
            _inputService.Moved -= OnMoved;
            _inputService.Fired -= OnFired;
        }

        private void OnMoved(Vector2 axis)
        {
            IsThrusting = axis.y > 0f;
            TurnValue = axis.x;
        }
        
        private void OnFired() =>
            Fired?.Invoke();
    }
}