using System;
using _Project.Core.Factories;
using _Project.Core.Signals;
using _Project.Features.Common.Signals;

namespace _Project.Features.Projectile
{
    public class ProjectileSpawner : IDisposable
    {
        private readonly IFactory<ProjectileSpawnData, ProjectileFacade> _factory;
        private readonly ISignalBus _signalBus;


        public ProjectileSpawner(
            IFactory<ProjectileSpawnData, ProjectileFacade> factory,
            ISignalBus signalBus)
        {
            _factory = factory;
            _signalBus = signalBus;
            _signalBus.Subscribe<SpawnRequestedSignal<ProjectileFacade>>(OnSpawnRequested);
        }

        private void OnSpawnRequested(SpawnRequestedSignal<ProjectileFacade> signal)
        {
            var initialMovementData = signal.initialMovementData;
            var spawnData = new ProjectileSpawnData(initialMovementData);
            _factory.Create(spawnData);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<SpawnRequestedSignal<ProjectileFacade>>(OnSpawnRequested);
        }
    }
}