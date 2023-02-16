using CodeBase.GameLogic.Asteroid;
using CodeBase.GameLogic.Bullets;
using CodeBase.GameLogic.Ship;
using CodeBase.Infrastructure.Configs;
using CodeBase.Infrastructure.Configs.Asteroids;
using CodeBase.Infrastructure.Services.ObstaclePlacement;
using CodeBase.Infrastructure.Services.Screen;
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
            Container.Bind<Camera>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesTo<ScreenService>().AsSingle();
            Container.BindInterfacesTo<ObstaclePlacementService>().AsSingle();
            
            BindConfigs();
            BindShip();
            BindFactory();
            BindSpawners();
            BindUi();
            BindGameStates();
        }
        
        private void BindFactory()
        {
            Container.BindMemoryPool<Ship, Ship.Pool>()
                .WithInitialSize(1)
                .FromComponentInNewPrefabResource("Prefabs/Ship");

            Container.BindFactory<Vector2, float, Sprite, AsteroidData, Asteroid, Asteroid.Factory>().FromMonoPoolableMemoryPool(
                    x => x.WithInitialSize(16).FromComponentInNewPrefabResource("Prefabs/Asteroid").UnderTransformGroup("Asteroids Pool"))
                .NonLazy();

            Container.BindMemoryPool<Bullet, Bullet.Pool>()
                .WithInitialSize(16)
                .FromComponentInNewPrefabResource("Prefabs/Bullet")
                .UnderTransformGroup("Bullets Pool")
                .NonLazy();
        }

        private void BindShip()
        {
            Container.BindInterfacesTo<ShipInput>().AsSingle();
            Container.BindInterfacesTo<ShipController>().AsSingle();
        }

        private void BindConfigs()
        {
            Container.Bind<AsteroidsConfig>().FromScriptableObjectResource("Configs/AsteroidsConfig").AsSingle();
            Container.Bind<ShipConfig>().FromScriptableObjectResource("Configs/ShipConfig").AsSingle();
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
        
        private void BindSpawners()
        {
            Container.Bind<IAsteroidsSpawner>().To<AsteroidsSpawner>().AsSingle();
            Container.Bind<IBulletsSpawner>().To<BulletsSpawner>().AsSingle();
        }
        
        private void BindUi()
        {
            Container.Bind<IGameUiScreen>().FromComponentInNewPrefabResource("Prefabs/Game UI Screen").AsSingle();
        }
    }
}
