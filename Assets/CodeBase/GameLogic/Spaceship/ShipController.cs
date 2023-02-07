using Zenject;

namespace CodeBase.GameLogic.Spaceship
{
    public class ShipController : IShipController, IFixedTickable
    {
        private readonly IShipInput _shipInput;
        private readonly Ship.Pool _shipPool;
        
        private IShip _ship;

        public ShipController(IShipInput shipInput, Ship.Pool shipPool)
        {
            _shipInput = shipInput;
            _shipPool = shipPool;
        }

        public void Spawn() =>
            _ship = _shipPool.Spawn();

        public void Despawn()
        {
            _shipPool.Despawn(_ship as Ship);
            _ship = null;
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
    }

    public interface IShipController
    {
        public void Spawn();
        public void Despawn();
    }
}