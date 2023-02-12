using System;
using UnityEngine;
using Zenject;

namespace CodeBase.GameLogic.Ship
{
    public class ShipController : IShipController, IFixedTickable
    {
        public event Action PlayerDead;
        
        private readonly IShipInput _shipInput;
        private readonly Ship.Factory _shipFactory;
        private readonly Camera _camera;

        private Ship _ship;

        public ShipController(IShipInput shipInput, Ship.Factory shipFactory, Camera camera)
        {
            _shipInput = shipInput;
            _shipFactory = shipFactory;
            _camera = camera;
        }

        public void Spawn()
        {
            _ship = _shipFactory.Create();
            _ship.Dead += OnPlayerDead;
        }

        public void Despawn()
        {
            _ship.Dispose();
            _ship.Dead -= OnPlayerDead;
        }

        public void FixedTick()
        {
            if (_ship == null)
                return;
            
            if (_shipInput.IsThrusting)
                _ship.AddThrust(3f);
            
            if (_shipInput.TurnValue != 0f)
                _ship.Turn(_shipInput.TurnValue, 0.35f);
        }
        
        private void OnPlayerDead() =>
            PlayerDead?.Invoke();
    }
}