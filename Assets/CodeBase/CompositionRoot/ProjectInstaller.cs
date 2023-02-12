using CodeBase.Infrastructure.Configs;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.SceneLoader;
using CodeBase.Infrastructure.StateMachine;
using Zenject;

namespace CodeBase.CompositionRoot
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(IInputService), typeof(IInitializable)).To<InputService>().AsSingle();

            Container.Bind<IAsteroidConfigService>().To<AsteroidConfigService>().AsSingle();
            
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            
            Container.Bind(typeof(IGameStateMachine), typeof(ITickable)).To<GameStateMachine>().AsSingle();
        }
    }
}