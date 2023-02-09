using CodeBase.GameLogic.Asteroid;
using CodeBase.GameLogic.Spaceship;

namespace CodeBase.Infrastructure.Services.Gameplay
{
    public class GameplayService
    {
        private readonly IShipController _shipController;
        private readonly IAsteroidsSpawner _asteroidsSpawner;

        public GameplayService(IShipController shipController, IAsteroidsSpawner asteroidsSpawner)
        {
            _shipController = shipController;
            _asteroidsSpawner = asteroidsSpawner;
        }
        
        public void StartGame()
        {
            _shipController.Spawn();
            _asteroidsSpawner.Spawn(10);
        }

        public void StopGame()
        {
            _shipController.Despawn();
            _asteroidsSpawner.DespawnAll();
        }
    }
}
