using System;
using CodeBase.GameLogic.Bullets;
using Zenject;

namespace CodeBase.GameLogic.Ship
{
    public class ShipController : IShipController, IFixedTickable
    {
        public event Action PlayerDead;
        
        private readonly IShipInput _shipInput;
        private readonly Ship.Factory _shipFactory;
        private readonly IBulletsSpawner _bulletsSpawner;

        private Ship _ship;

        public ShipController(IShipInput shipInput,
            Ship.Factory shipFactory,
            IBulletsSpawner bulletsSpawner)
        {
            _shipInput = shipInput;
            _shipFactory = shipFactory;
            _bulletsSpawner = bulletsSpawner;
        }

        public void Spawn()
        {
            _ship = _shipFactory.Create();
            _ship.Dead += OnPlayerDead;
            _shipInput.Fired += OnFired;
        }

        public void Despawn()
        {
            _shipInput.Fired -= OnFired;
            _bulletsSpawner.DespawnAll();
            _ship.Dead -= OnPlayerDead;
            _ship.Dispose();
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
        
        private void OnFired() => 
            _bulletsSpawner.Spawn(_ship.transform);

        private void OnPlayerDead() =>
            PlayerDead?.Invoke();
    }
}