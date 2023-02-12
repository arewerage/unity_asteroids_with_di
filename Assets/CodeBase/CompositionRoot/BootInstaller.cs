using CodeBase.Infrastructure.StateMachine;
using CodeBase.Infrastructure.StateMachine.States;
using Zenject;

namespace CodeBase.CompositionRoot
{
    public class BootInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindFactory<BootstrapState, BootstrapState.Factory>();

            Container.Resolve<IGameStateMachine>().RegisterState(Container.Resolve<BootstrapState.Factory>().Create());
        }
    }
}
