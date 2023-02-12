using CodeBase.GameLogic.Asteroid;
using CodeBase.GameLogic.Bullet;
using CodeBase.GameLogic.Ship;
using CodeBase.Infrastructure.Configs;
using CodeBase.Infrastructure.StateMachine;
using CodeBase.Infrastructure.StateMachine.States;
using CodeBase.UI;
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

            Container.Bind<IAsteroidsSpawner>().To<AsteroidsSpawner>().AsSingle();

            Container.Bind<IGameUiScreen>().FromComponentInNewPrefabResource("Prefabs/Game UI Screen").AsSingle();

            Container.Bind<Camera>().FromInstance(Camera.main).AsSingle();
            
            BindGameStates();
        }

        private void BindGameStates()
        {
            Container.BindFactory<MainMenuState, MainMenuState.Factory>();
            Container.BindFactory<GameplayState, GameplayState.Factory>();
            Container.BindFactory<GameOverState, GameOverState.Factory>();
            
            IGameStateMachine gameStateMachine = Container.Resolve<IGameStateMachine>();
            
            gameStateMachine.RegisterState(Container.Resolve<MainMenuState.Factory>().Create());
            gameStateMachine.RegisterState(Container.Resolve<GameplayState.Factory>().Create());
            gameStateMachine.RegisterState(Container.Resolve<GameOverState.Factory>().Create());
        }
    }
}
