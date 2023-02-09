using CodeBase.GameLogic.Asteroid;
using CodeBase.GameLogic.Bullet;
using CodeBase.GameLogic.Spaceship;
using CodeBase.Infrastructure.Configs;
using UnityEngine;
using Zenject;

namespace CodeBase.CompositionRoot
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ShipInput>().AsSingle();

            Container.BindFactory<Ship, Ship.Factory>().FromMonoPoolableMemoryPool(
                x => x.WithInitialSize(1).FromComponentInNewPrefabResource("Prefabs/Ship"));

            Container.BindFactory<Vector2, float, AsteroidConfig, Asteroid, Asteroid.Factory>().FromMonoPoolableMemoryPool(
                x => x.WithInitialSize(32).FromComponentInNewPrefabResource("Prefabs/Asteroid").UnderTransformGroup("Asteroids Pool")).NonLazy();
            
            Container.BindMemoryPool<Bullet, Bullet.Pool>()
                .WithInitialSize(32)
                .FromComponentInNewPrefabResource("Prefabs/Bullet")
                .UnderTransformGroup("Bullets Pool")
                .NonLazy();

            Container.BindInterfacesTo<ShipController>().AsSingle();
        }
    }
}
