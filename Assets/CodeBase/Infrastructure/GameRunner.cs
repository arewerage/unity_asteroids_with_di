using CodeBase.Infrastructure.StateMachine;
using CodeBase.Infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        private IGameStateMachine _gameStateMachine;
        
        [Inject]
        private void Construct(IGameStateMachine gameStateMachine) =>
            _gameStateMachine = gameStateMachine;

        private void Awake() => 
            _gameStateMachine.Enter<BootstrapState>();
    }
}