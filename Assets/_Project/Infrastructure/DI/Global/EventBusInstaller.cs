using _Project.Core.EventBus;
using _Project.Core.Physics.Collision.Events;
using _Project.Features.Asteroid;
using _Project.Features.Common.EntitiesLifecycle.Events;
using _Project.Features.Common.Hit.Events;
using _Project.Features.Spaceship;
using _Project.Features.Spaceship.Events;
using _Project.Features.Spaceship.Weapon.LaserWeapon.LaserBeam;
using _Project.Features.Spaceship.Weapon.ProjectileWeapon.Projectile;
using _Project.Features.UFO;
using _Project.Features.UI.Common.Events;
using _Project.Infrastructure.EventBus;
using Zenject;

namespace _Project.Infrastructure.DI.Global
{
    public class EventBusInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindEventBus();
            
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
        }
        
        private void BindEventBus()
        {
            SignalBusInstaller.Install(Container);
            
            Container
                .Bind<IEventBus>()
                .To<ZenjectSignalBus>()
                .AsSingle()
                .NonLazy();
        }
    }
}