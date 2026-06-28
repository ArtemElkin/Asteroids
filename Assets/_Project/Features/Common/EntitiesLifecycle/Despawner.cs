using System;
using _Project.Core.EventBus;
using _Project.Core.Factories;
using _Project.Core.GameLifecycle;
using _Project.Core.Tools;
using _Project.Features.Common.EntitiesLifecycle.Events;

namespace _Project.Features.Common.EntitiesLifecycle
{
    public class Despawner<TFacade> : IWorldResettable, IDisposable where TFacade : class, IFacade
    {
        private readonly IReleaser<TFacade> _releaser;
        private readonly Storage<TFacade> _storage;
        private readonly IEventBus _eventBus;


        public Despawner(
            IReleaser<TFacade> releaser,
            Storage<TFacade> storage,
            IEventBus eventBus)
        {
            _releaser =  releaser;
            _storage = storage;
            _eventBus = eventBus;
            _eventBus.Subscribe<DespawnRequestedEvent<TFacade>>(OnDespawnRequested);
        }

        public void Reset()
        {
            DespawnAll();
        }

        private void DespawnAll()
        {
            foreach (var facade in _storage)
            {
                _releaser.Release(facade);
            }
            _storage.Clear();
        }

        private void OnDespawnRequested(DespawnRequestedEvent<TFacade> @event)
        {
            var facade = (TFacade)@event.facade;
            _releaser.Release(facade);
            _storage.Remove(facade);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<DespawnRequestedEvent<TFacade>>(OnDespawnRequested);;
        }
    }
}