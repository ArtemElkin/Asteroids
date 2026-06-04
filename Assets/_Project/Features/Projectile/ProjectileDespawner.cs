using System;
using _Project.Core.Factories;
using _Project.Core.Signals;
using _Project.Features.Common.Signals;

namespace _Project.Features.Projectile
{
    public class ProjectileDespawner : IDisposable
    {
        private readonly IFactory<ProjectileSpawnData, ProjectileFacade> _projectileFactory;
        private readonly ISignalBus _signalBus;


        public ProjectileDespawner(
            IFactory<ProjectileSpawnData, ProjectileFacade> projectileFactory,
            ISignalBus signalBus)
        {
            _projectileFactory =  projectileFactory;
            _signalBus = signalBus;
            
            _signalBus.Subscribe<DespawnRequestedSignal<ProjectileFacade>>(OnDespawnRequested);
        }

        private void OnDespawnRequested(DespawnRequestedSignal<ProjectileFacade> signal)
        {
            var projectileToDespawn = signal.facade;
            _projectileFactory.Release((ProjectileFacade)projectileToDespawn);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<DespawnRequestedSignal<ProjectileFacade>>(OnDespawnRequested);;
        }
    }
}