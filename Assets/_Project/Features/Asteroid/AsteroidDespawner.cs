using System;
using _Project.Core.Factories;
using _Project.Core.Signals;
using _Project.Core.Tools;
using _Project.Features.Common.Signals;

namespace _Project.Features.Asteroid
{
    public class AsteroidDespawner : IDisposable
    {
        private readonly IFactory<AsteroidSpawnData, AsteroidFacade> _factory;
        private readonly Storage<AsteroidFacade> _storage;
        private readonly ISignalBus _signalBus;


        public AsteroidDespawner(
            IFactory<AsteroidSpawnData, AsteroidFacade> factory,
            Storage<AsteroidFacade> storage,
            ISignalBus signalBus)
        {
            _factory =  factory;
            _storage = storage;
            _signalBus = signalBus;
            
            _signalBus.Subscribe<DespawnRequestedSignal<AsteroidFacade>>(OnDespawnRequested);
        }

        private void OnDespawnRequested(DespawnRequestedSignal<AsteroidFacade> signal)
        {
            var asteroidToDespawn = (AsteroidFacade)signal.facade;
            _factory.Release(asteroidToDespawn);
            _storage.Remove(asteroidToDespawn);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<DespawnRequestedSignal<AsteroidFacade>>(OnDespawnRequested);;
        }
    }
}