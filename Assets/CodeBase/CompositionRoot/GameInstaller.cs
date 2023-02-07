using CodeBase.Configs;
using CodeBase.GameLogic.Asteroid;
using CodeBase.GameLogic.Bullet;
using CodeBase.GameLogic.Spaceship;
using Zenject;

namespace CodeBase.CompositionRoot
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ShipInput>().AsSingle();
            
            Container.Bind<AsteroidsConfig>().FromScriptableObjectResource("AsteroidsConfig").AsSingle();

            Container.BindMemoryPool<Ship, Ship.Pool>()
                .WithInitialSize(1)
                .WithMaxSize(1)
                .FromComponentInNewPrefabResource("Ship")
                .NonLazy();
            
            Container.BindMemoryPool<Asteroid, Asteroid.Pool>()
                .WithInitialSize(32)
                .FromComponentInNewPrefabResource("Asteroid")
                .UnderTransformGroup("Asteroids Pool")
                .NonLazy();
            
            Container.BindMemoryPool<Bullet, Bullet.Pool>()
                .WithInitialSize(32)
                .FromComponentInNewPrefabResource("Bullet")
                .UnderTransformGroup("Bullets Pool")
                .NonLazy();

            Container.BindInterfacesTo<ShipController>().AsSingle();
        }
    }
}
