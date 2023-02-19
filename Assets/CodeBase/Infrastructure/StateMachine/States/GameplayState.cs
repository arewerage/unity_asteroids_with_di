using CodeBase.GameLogic.Asteroid;
using CodeBase.GameLogic.Ship;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.UI;
using Zenject;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class GameplayState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IShipController _shipController;
        private readonly IAsteroidsController _asteroidsController;
        private readonly IInputService _inputService;

        public GameplayState(IGameStateMachine gameStateMachine,
            IShipController shipController,
            IAsteroidsController asteroidsController,
            IInputService inputService)
        {
            _gameStateMachine = gameStateMachine;
            _shipController = shipController;
            _asteroidsController = asteroidsController;
            _inputService = inputService;
        }

        public void Enter()
        {
            _inputService.EnableGameplay();
            _shipController.Spawn();
            _asteroidsController.Spawn();

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
