using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine, ITickable
    {
        private readonly Dictionary<Type, IExitableState> _states;

        private IExitableState _activeState;
        private ITickable _activeTickableState;

        public GameStateMachine() =>
            _states = new Dictionary<Type, IExitableState>();

        public void Enter<TState>() where TState : class, IState
        {
            TState newState = ChangeState<TState>();
            newState.Enter();
        }
        
        public void Enter<TState, TArg>(TArg arg) where TState : class, IStateWithArgument<TArg>
        {
            TState newState = ChangeState<TState>();
            newState.Enter(arg);
        }
        
        public void RegisterState<TState>(TState state) where TState : IExitableState =>
            _states.Add(typeof(TState), state);
        
        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
      
            TState state = GetState<TState>();
            _activeState = state;
            _activeTickableState = _activeState as ITickable;
      
            return state;
        }
    
        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;

        public void Tick() =>
            _activeTickableState?.Tick();
    }
}
