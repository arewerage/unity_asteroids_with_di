using System;
using CodeBase.Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace CodeBase.GameLogic.Ship
{
    public class ShipInput : IShipInput, IInitializable, IDisposable
    {
        private readonly IInputService _inputService;

        public bool IsThrusting { get; private set; }
        public float TurnValue { get; private set; }
        
        public ShipInput(IInputService inputService) =>
            _inputService = inputService;

        public void Initialize() =>
            _inputService.Moved += OnMoved;

        public void Dispose() =>
            _inputService.Moved -= OnMoved;

        private void OnMoved(Vector2 axis)
        {
            IsThrusting = axis.y > 0f;
            TurnValue = axis.x;
        }
    }
}