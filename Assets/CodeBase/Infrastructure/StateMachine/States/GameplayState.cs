using CodeBase.GameLogic.Asteroid;
using CodeBase.GameLogic.Ship;
using CodeBase.Infrastructure.Configs;
using CodeBase.Infrastructure.Services.Input;
using Zenject;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class GameplayState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IAsteroidConfigService _asteroidConfigService;
        private readonly IShipController _shipController;
        private readonly IAsteroidsSpawner _asteroidsSpawner;
        private readonly IInputService _inputService;

        public GameplayState(IGameStateMachine gameStateMachine,
            IAsteroidConfigService asteroidConfigService,
            IShipController shipController,
            IAsteroidsSpawner asteroidsSpawner,
            IInputService inputService)
        {
            _gameStateMachine = gameStateMachine;
            _asteroidConfigService = asteroidConfigService;
            _shipController = shipController;
            _asteroidsSpawner = asteroidsSpawner;
            _inputService = inputService;
        }

        public void Enter()
        {
            _inputService.EnableGameplay();
            _asteroidConfigService.LoadAll();
            _asteroidsSpawner.DespawnAll();
            _shipController.Spawn();
            _asteroidsSpawner.Spawn(5);

            _shipController.PlayerDead += OnGameLose;
        }
        
        public void Exit()
        {
            _shipController.PlayerDead -= OnGameLose;
            _shipController.Despawn();
        }

        private void OnGameLose() =>
            _gameStateMachine.Enter<GameOverState>();

        public class Factory : PlaceholderFactory<GameplayState>
        {
        }
    }
}
