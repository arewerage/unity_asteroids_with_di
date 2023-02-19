using System;
using CodeBase.GameLogic.Bullets;
using CodeBase.Infrastructure.Configs;
using UnityEngine;
using Zenject;

namespace CodeBase.GameLogic.Ship
{
    public class ShipController : IShipController, IFixedTickable
    {
        public event Action PlayerDead;
        
        private readonly IShipInput _shipInput;
        private readonly Ship.Pool _shipPool;
        private readonly IBulletsSpawner _bulletsSpawner;
        private readonly Camera _gameCamera;
        private readonly ShipConfig _shipConfig;

        private Ship _ship;

        public ShipController(IShipInput shipInput,
            Ship.Pool shipPool,
            IBulletsSpawner bulletsSpawner,
            Camera gameCamera,
            ShipConfig shipConfig)
        {
            _shipInput = shipInput;
            _shipPool = shipPool;
            _bulletsSpawner = bulletsSpawner;
            _gameCamera = gameCamera;
            _shipConfig = shipConfig;
        }

        public void Spawn()
        {
            _ship = _shipPool.Spawn();
            _ship.Dead += OnPlayerDead;
            _shipInput.Fired += OnFired;
        }

        public void Despawn()
        {
            _shipInput.Fired -= OnFired;
            _bulletsSpawner.DespawnAll();
            
            _ship.Dead -= OnPlayerDead;
            _shipPool.Despawn(_ship);

            _ship = null;
        }

        public void FixedTick()
        {
            if (_ship == null)
                return;
            
            if (_shipInput.IsThrusting)
                _ship.AddThrust(_shipConfig.ThrustingForce);
            
            if (_shipInput.TurnValue != 0f)
                _ship.Turn(_shipInput.TurnValue, _shipConfig.TurnSpeed);
            
            _ship.WrapScreen(_gameCamera);
        }
        
        private void OnFired() => 
            _bulletsSpawner.Spawn(_ship.transform);

        private void OnPlayerDead() =>
            PlayerDead?.Invoke();
    }
}