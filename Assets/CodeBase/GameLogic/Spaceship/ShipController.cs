using Zenject;

namespace CodeBase.GameLogic.Spaceship
{
    public class ShipController : IShipController, IFixedTickable
    {
        private readonly IShipInput _shipInput;
        private readonly Ship.Factory _shipFactory;
        
        private Ship _ship;

        public ShipController(IShipInput shipInput, Ship.Factory shipFactory)
        {
            _shipInput = shipInput;
            _shipFactory = shipFactory;
        }

        public void Spawn() =>
            _ship = _shipFactory.Create();

        public void Despawn() =>
            _ship.Dispose();

        public void FixedTick()
        {
            if (_ship == null)
                return;
            
            if (_shipInput.IsThrusting)
                _ship.AddThrust(3f);
            
            if (_shipInput.TurnValue != 0f)
                _ship.Turn(_shipInput.TurnValue, 0.35f);
        }
    }

    public interface IShipController
    {
        public void Spawn();
        public void Despawn();
    }
}