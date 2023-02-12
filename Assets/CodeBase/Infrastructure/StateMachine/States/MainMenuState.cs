using CodeBase.Infrastructure.Services.Input;
using CodeBase.UI;
using Zenject;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class MainMenuState : IState
    {
        private const string MenuTitle = "Asteroids!";
        private const string InputHint = "Press space for play";
        
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IInputService _inputService;
        private readonly IGameUiScreen _gameUiScreen;

        public MainMenuState(IGameStateMachine gameStateMachine,
            IInputService inputService,
            IGameUiScreen gameUiScreen)
        {
            _gameStateMachine = gameStateMachine;
            _inputService = inputService;
            _gameUiScreen = gameUiScreen;
        }

        public void Enter()
        {
            _inputService.EnableUI();
            _inputService.Played += OnStartGame;
            _gameUiScreen.ShowWithTitle(MenuTitle, InputHint);
        }

        public void Exit()
        {
            _inputService.Played -= OnStartGame;
            _gameUiScreen.Hide();
        }
        
        private void OnStartGame() =>
            _gameStateMachine.Enter<GameplayState>();

        public class Factory : PlaceholderFactory<MainMenuState>
        {
        }
    }
}