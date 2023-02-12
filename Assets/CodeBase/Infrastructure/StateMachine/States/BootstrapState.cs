using CodeBase.Infrastructure.Services.SceneLoader;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private const string GameSceneName = "Game";
        
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;

        public BootstrapState(IGameStateMachine gameStateMachine,
            ISceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            SceneRequest sceneRequest = new SceneRequest
            {
                Name = GameSceneName,
                Mode = LoadSceneMode.Single,
                OnLoaded = OnGameSceneLoaded
            };

            _sceneLoader.LoadSceneAsync(sceneRequest);
        }

        public void Exit()
        {
        }
        
        private void OnGameSceneLoaded() =>
            _gameStateMachine.Enter<MainMenuState>();

        public class Factory : PlaceholderFactory<BootstrapState>
        {
        }
    }
}