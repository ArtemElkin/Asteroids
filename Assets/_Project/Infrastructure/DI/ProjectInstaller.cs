using _Project.Core.Audio;
using _Project.Core.Config;
using _Project.Core.EventBus;
using _Project.Core.Physics.Collision.Events;
using _Project.Core.Player;
using _Project.Core.Save;
using _Project.Core.Services;
using _Project.Features.Asteroid;
using _Project.Features.Common.EntitiesLifecycle.Events;
using _Project.Features.Common.Hit.Events;
using _Project.Features.Common.Settings;
using _Project.Features.Spaceship;
using _Project.Features.Spaceship.Events;
using _Project.Features.Spaceship.Weapon.LaserWeapon.LaserBeam;
using _Project.Features.Spaceship.Weapon.ProjectileWeapon.Projectile;
using _Project.Features.UFO;
using _Project.Features.UI.Common.Events;
using _Project.Infrastructure.Ads;
using _Project.Infrastructure.Analytics;
using _Project.Infrastructure.Audio;
using _Project.Infrastructure.EventBus;
using _Project.Infrastructure.Input;
using _Project.Infrastructure.UnityServices;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.DI
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _audioServicePrefab;
        
        
        public override void InstallBindings()
        {
            BindSignalBus();
            
            Container.DeclareSignal<StartGameClickedEvent>();
            Container.DeclareSignal<MainMenuClickedEvent>();
            Container.DeclareSignal<SettingsClickedEvent>();
            Container.DeclareSignal<BackToMenuClickedEvent>();
            
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
            BindSettingsModel();
            BindSettingsSaveController();
            BindAudioService(_audioServicePrefab);
            BindSceneLoadService();
            BindAdsService();
            BindAnalyticsService();
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
                .BindInterfacesAndSelfTo<PlayerSaveController>()
                .AsSingle()
                .NonLazy();
        }

        private void BindSettingsModel()
        {
            Container
                .Bind<SettingsModel>()
                .AsSingle()
                .NonLazy();
        }

        private void BindSettingsSaveController()
        {
            Container
                .BindInterfacesAndSelfTo<SettingsSaveController>()
                .AsSingle()
                .NonLazy();
        }

        private void BindAudioService(GameObject audioServicePrefab)
        {
            Container
                .BindInterfacesAndSelfTo<AudioService>()
                .FromComponentInNewPrefab(audioServicePrefab)
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
            Container
                .BindInterfacesTo<YandexAdsService>()
                .AsSingle()
                .NonLazy();
#endif
        }

        private void BindAnalyticsService()
        {
#if UNITY_EDITOR
            Container
                .BindInterfacesTo<MockAnalyticsService>()
                .AsSingle()
                .NonLazy();
#else
            Container
                .BindInterfacesTo<FirebaseAnalyticsService>()
                .AsSingle()
                .NonLazy();
#endif
        }
    }
}