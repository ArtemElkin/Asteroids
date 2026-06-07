using System;
using _Project.Core.Factories;
using _Project.Core.Signals;
using _Project.Features.Common.Signals;

namespace _Project.Features.Common.Clone
{
    public class CloneDespawner<TOriginFacade> : IDisposable where TOriginFacade : IFacade
    {
        private readonly IFactory<CloneSpawnData, CloneFacade<TOriginFacade>> _factory;
        private readonly CloneStorage<TOriginFacade> _storage;
        private readonly ISignalBus _signalBus;


        public CloneDespawner(
            IFactory<CloneSpawnData, CloneFacade<TOriginFacade>> factory,
            CloneStorage<TOriginFacade> storage,
            ISignalBus signalBus)
        {
            _factory = factory;
            _storage = storage;
            _signalBus = signalBus;
            
            _signalBus.Subscribe<CloneDespawnRequestedSignal<TOriginFacade>>(OnDespawnRequested);
        }

        private void OnDespawnRequested(CloneDespawnRequestedSignal<TOriginFacade> signal)
        {
            var origin = signal.originFacade;
            var clones = _storage.GetAllClones(origin);
            foreach (var clone in clones)
            {
                _factory.Release(clone);
                _storage.RemoveClone(origin, clone);
            }
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<CloneDespawnRequestedSignal<TOriginFacade>>(OnDespawnRequested);
        }
    }
}