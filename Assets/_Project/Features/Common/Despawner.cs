using System;
using _Project.Core.EventBus;
using _Project.Core.Factories;
using _Project.Core.Tools;
using _Project.Features.Common.Event;

namespace _Project.Features.Common
{
    public class Despawner<TFacade> : IDisposable where TFacade : class, IFacade
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

        private void OnDespawnRequested(DespawnRequestedEvent<TFacade> @event)
        {
            var facade = (TFacade)@event.facade;
            _releaser.Release(facade);
            _storage.Remove(facade);
        }

        public void Dispose()
        {
            foreach (var facade in _storage)
            {
                _releaser.Release(facade);
            }
            _storage.Clear();
            _eventBus.Unsubscribe<DespawnRequestedEvent<TFacade>>(OnDespawnRequested);;
        }
    }
}