using System;
using _Project.Core.Factories;
using _Project.Core.Signals;
using _Project.Core.Tools;
using _Project.Features.Common.Signals;

namespace _Project.Features.Spaceship
{
    public class SpaceshipDespawner : IDisposable
    {
        private readonly IFactory<SpaceshipSpawnData, SpaceshipFacade> _factory;
        private readonly Storage<SpaceshipFacade> _storage;
        private readonly ISignalBus _signalBus;


        public SpaceshipDespawner(
            IFactory<SpaceshipSpawnData, SpaceshipFacade> factory,
            Storage<SpaceshipFacade> storage,
            ISignalBus signalBus)
        {
            _factory =  factory;
            _storage = storage;
            _signalBus = signalBus;
            
            _signalBus.Subscribe<DespawnRequestedSignal<SpaceshipFacade>>(OnDespawnRequested);
        }

        private void OnDespawnRequested(DespawnRequestedSignal<SpaceshipFacade> signal)
        {
            var spaceshipToDespawn = (SpaceshipFacade)signal.facade;
            _factory.Release(spaceshipToDespawn);
            _storage.Remove(spaceshipToDespawn);
            _signalBus.Fire(new CloneDespawnRequestedSignal<SpaceshipFacade>(spaceshipToDespawn));
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<DespawnRequestedSignal<SpaceshipFacade>>(OnDespawnRequested);;
        }
    }
}