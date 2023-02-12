using CodeBase.GameLogic.Asteroid;
using CodeBase.GameLogic.Ship;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.UI;
using Zenject;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class GameOverState : IState
    {
        private const string OverTitle = "Game Over!";
        private const string InputHint = "Press space for restart";
        
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IInputService _inputService;
        private readonly IGameUiScreen _gameUiScreen;
        private readonly IShipController _shipController;
        private readonly IAsteroidsSpawner _asteroidsSpawner;

        public GameOverState(IGameStateMachine gameStateMachine,
            IInputService inputService,
            IGameUiScreen gameUiScreen,
            IShipController shipController,
            IAsteroidsSpawner asteroidsSpawner)
        {
            _gameStateMachine = gameStateMachine;
            _inputService = inputService;
            _gameUiScreen = gameUiScreen;
            _shipController = shipController;
            _asteroidsSpawner = asteroidsSpawner;
        }
        
        public void Enter()
        {
            _gameUiScreen.ShowWithTitle(OverTitle, InputHint);
            _inputService.EnableUI();
            _inputService.Played += OnRestartGame;
        }

        public void Exit()
        {
            _gameUiScreen.Hide();
            _inputService.Played -= OnRestartGame;
            _shipController.Despawn();
            _asteroidsSpawner.DespawnAll();
        }
        
        private void OnRestartGame() =>
            _gameStateMachine.Enter<GameplayState>();

        public class Factory : PlaceholderFactory<GameOverState>
        {
        }
    }
}
