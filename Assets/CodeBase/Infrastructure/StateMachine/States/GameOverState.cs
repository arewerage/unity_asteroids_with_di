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

        public GameOverState(IGameStateMachine gameStateMachine,
            IInputService inputService,
            IGameUiScreen gameUiScreen)
        {
            _gameStateMachine = gameStateMachine;
            _inputService = inputService;
            _gameUiScreen = gameUiScreen;
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
        }
        
        private void OnRestartGame() =>
            _gameStateMachine.Enter<GameplayState>();

        public class Factory : PlaceholderFactory<GameOverState>
        {
        }
    }
}
