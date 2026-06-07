using System;
using _Project.Core.Factories;
using _Project.Core.Signals;
using _Project.Core.Tools;
using _Project.Features.Common.Signals;

namespace _Project.Features.UFO
{
    public class UFODespawner : IDisposable
    {
        private readonly IFactory<UFOSpawnData, UFOFacade> _factory;
        private readonly Storage<UFOFacade> _storage;
        private readonly ISignalBus _signalBus;


        public UFODespawner(
            IFactory<UFOSpawnData, UFOFacade> factory,
            Storage<UFOFacade> storage,
            ISignalBus signalBus)
        {
            _factory =  factory;
            _storage = storage;
            _signalBus = signalBus;
            
            _signalBus.Subscribe<DespawnRequestedSignal<UFOFacade>>(OnDespawnRequested);
        }

        private void OnDespawnRequested(DespawnRequestedSignal<UFOFacade> signal)
        {
            var ufoToDespawn = signal.facade;
            _factory.Release((UFOFacade)ufoToDespawn);
            _storage.Remove((UFOFacade)ufoToDespawn);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<DespawnRequestedSignal<UFOFacade>>(OnDespawnRequested);;
        }
    }
}