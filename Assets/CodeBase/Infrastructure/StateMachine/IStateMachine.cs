namespace CodeBase.Infrastructure.StateMachine
{
    public interface IStateMachine
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TArg>(TArg arg) where TState : class, IStateWithArgument<TArg>;
        void RegisterState<TState>(TState state) where TState : IExitableState;
    }
}
