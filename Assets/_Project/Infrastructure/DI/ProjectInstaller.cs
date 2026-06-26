using _Project.Core.Ads;
using _Project.Core.Config;
using _Project.Core.EventBus;
using _Project.Core.GameLifecycle.Events;
using _Project.Core.Physics.Collision.Events;
using _Project.Core.Player;
using _Project.Core.Save;
using _Project.Core.Services;
using _Project.Features.Asteroid;
using _Project.Features.Common.EntitiesLifecycle.Events;
using _Project.Features.Common.Hit.Events;
using _Project.Features.Spaceship;
using _Project.Features.Spaceship.Events;
using _Project.Features.Spaceship.Weapon.LaserWeapon.LaserBeam;
using _Project.Features.Spaceship.Weapon.ProjectileWeapon.Projectile;
using _Project.Features.UFO;
using _Project.Features.UI.Common.Events;
using _Project.Infrastructure.Ads;
using _Project.Infrastructure.EventBus;
using _Project.Infrastructure.Input;
using _Project.Infrastructure.UnityServices;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.DI
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _inputHandlerPrefab;
        
        
        public override void InstallBindings()
        {
            BindSignalBus();
            
            Container.DeclareSignal<SceneInitializeEvent>();
            
            Container.DeclareSignal<StartGameClickedEvent>();
            Container.DeclareSignal<MainMenuClickedEvent>();
            
            Container.DeclareSignal<DespawnRequestedEvent<SpaceshipFacade>>();
            Container.DeclareSignal<SpawnRequestedEvent<AsteroidFacade>>();
            Container.DeclareSignal<DespawnRequestedEvent<AsteroidFacade>>();
            Container.DeclareSignal<DespawnRequestedEvent<UFOFacade>>();
            Container.DeclareSignal<SpawnRequestedEvent<ProjectileFacade>>();
            Container.DeclareSignal<DespawnRequestedEvent<ProjectileFacade>>();
            Container.DeclareSignal<DespawnRequestedEvent<LaserBeamFacade>>();
            
            Container.DeclareSignal<CollisionDetectedEvent>();
            Container.DeclareSignal<CollisionProcessedEvent>();
            Container.DeclareSignal<HitEvent>();

            Container.DeclareSignal<SpaceshipSpawnedEvent>();
            Container.DeclareSignal<EnemyDestroyedEvent>();

            BindTimeService();
            BindTimer();
            BindRandomService();
            BindSaveService();
            BindConfigProviders();
            BindPlayerModel();
            BindPlayerSaveController();
            BindInput();
            BindSceneLoadService();
            BindAdsService();
        }

        private void BindTimeService()
        {
            Container
                .Bind<ITimeService>()
                .To<UnityTimeService>()
                .FromNewComponentOnNewGameObject()
                .AsSingle()
                .NonLazy();
        }

        private void BindTimer()
        {
            Container
                .BindInterfacesAndSelfTo<Timer>()
                .AsTransient();
        }

        private void BindSignalBus()
        {
            SignalBusInstaller.Install(Container);
            
            Container
                .Bind<IEventBus>()
                .To<ZenjectSignalBus>()
                .AsSingle()
                .NonLazy();
        }

        private void BindRandomService()
        {
            Container
                .Bind<IRandomService>()
                .To<RandomService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindSaveService()
        {
            Container
                .Bind<ISaveService>()
                .To<PlayerPrefsSaveService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindConfigProviders()
        {
            Container
                .Bind<IConfigProvider>()
                .To<ResourcesConfigProvider>()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerModel()
        {
            Container
                .Bind<PlayerModel>()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerSaveController()
        {
            Container
                .Bind<PlayerSaveController>()
                .AsSingle()
                .NonLazy();
        }

        private void BindInput()
        {
            Container
                .BindInterfacesTo<StandaloneInputHandler>()
                .FromComponentInNewPrefab(_inputHandlerPrefab)
                .AsSingle()
                .NonLazy();
        }

        private void BindSceneLoadService()
        {
            Container
                .BindInterfacesAndSelfTo<SceneLoadService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindAdsService()
        {
#if UNITY_EDITOR
            Container
                .BindInterfacesTo<MockAdsService>()
                .AsSingle()
                .NonLazy();
#else
            // Container
            //     .BindInterfacesfTo<YandexAdsService>()
            //     .AsSingle()
            //     .NonLazy();
#endif
        }
    }
}