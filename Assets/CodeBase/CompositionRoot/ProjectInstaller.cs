using CodeBase.Infrastructure.Configs;
using CodeBase.Infrastructure.Services.Input;
using Zenject;

namespace CodeBase.CompositionRoot
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(IInputService), typeof(IInitializable)).To<InputService>().AsSingle().NonLazy();
            
            Container.Bind(typeof(IAsteroidConfigService), typeof(IInitializable)).To<AsteroidConfigService>().AsSingle().NonLazy();
        }
    }
}