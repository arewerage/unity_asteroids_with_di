using CodeBase.GameLogic.Asteroid;
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
        private readonly IMenuUiScreen _menuUiScreen;
        private readonly IAsteroidsController _asteroidsController;

        public GameOverState(IGameStateMachine gameStateMachine,
            IInputService inputService,
            IMenuUiScreen menuUiScreen,
            IAsteroidsController asteroidsController)
        {
            _gameStateMachine = gameStateMachine;
            _inputService = inputService;
            _menuUiScreen = menuUiScreen;
            _asteroidsController = asteroidsController;
        }
        
        public void Enter()
        {
            _menuUiScreen.ShowWithTitle(OverTitle, InputHint);
            _inputService.EnableUI();
            _inputService.Played += OnRestartGame;
        }

        public void Exit()
        {
            _menuUiScreen.Hide();
            _inputService.Played -= OnRestartGame;
            _asteroidsController.DespawnAll();
        }
        
        private void OnRestartGame() =>
            _gameStateMachine.Enter<GameplayState>();

        public class Factory : PlaceholderFactory<GameOverState>
        {
        }
    }
}
