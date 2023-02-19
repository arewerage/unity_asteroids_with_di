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
        private readonly IMenuUiScreen _menuUiScreen;

        public MainMenuState(IGameStateMachine gameStateMachine,
            IInputService inputService,
            IMenuUiScreen menuUiScreen)
        {
            _gameStateMachine = gameStateMachine;
            _inputService = inputService;
            _menuUiScreen = menuUiScreen;
        }

        public void Enter()
        {
            _inputService.EnableUI();
            _inputService.Played += OnStartGame;
            _menuUiScreen.ShowWithTitle(MenuTitle, InputHint);
        }

        public void Exit()
        {
            _inputService.Played -= OnStartGame;
            _menuUiScreen.Hide();
        }
        
        private void OnStartGame() =>
            _gameStateMachine.Enter<GameplayState>();

        public class Factory : PlaceholderFactory<MainMenuState>
        {
        }
    }
}