namespace CodeBase.Infrastructure.StateMachine
{
    public interface IStateWithArgument<in TArg> : IExitableState
    {
        void Enter(TArg arg);
    }
}
