using System;
using _Project.Core.Factories;
using _Project.Core.Signals;
using _Project.Core.Tools;
using _Project.Features.Common.Signals;

namespace _Project.Features.Asteroid
{
    public class AsteroidDespawner : IDisposable
    {
        private readonly IFactory<AsteroidSpawnData, AsteroidFacade> _asteroidFactory;
        private readonly Storage<AsteroidFacade> _asteroidStorage;
        private readonly ISignalBus _signalBus;


        public AsteroidDespawner(
            IFactory<AsteroidSpawnData, AsteroidFacade> asteroidFactory,
            Storage<AsteroidFacade> asteroidStorage,
            ISignalBus signalBus)
        {
            _asteroidFactory =  asteroidFactory;
            _asteroidStorage = asteroidStorage;
            _signalBus = signalBus;
            
            _signalBus.Subscribe<DespawnRequestedSignal<AsteroidFacade>>(OnDespawnRequested);
        }

        private void OnDespawnRequested(DespawnRequestedSignal<AsteroidFacade> signal)
        {
            var asteroidToDespawn = (AsteroidFacade)signal.facade;
            _asteroidFactory.Release(asteroidToDespawn);
            _asteroidStorage.Remove(asteroidToDespawn);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<DespawnRequestedSignal<AsteroidFacade>>(OnDespawnRequested);;
        }
    }
}