using CodeBase.GameLogic.Spaceship;
using CodeBase.Infrastructure.Services.Input;
using Zenject;

namespace CodeBase.CompositionRoot
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(IInputService), typeof(IInitializable)).To<InputService>().AsSingle().NonLazy();

            Container.BindInterfacesTo<ShipInput>().AsSingle();

            Container.BindMemoryPool<Ship, Ship.Pool>()
                .WithInitialSize(1)
                .WithMaxSize(1)
                .FromComponentInNewPrefabResource("Ship");

            Container.BindInterfacesTo<ShipController>().AsSingle();
        }
    }
}