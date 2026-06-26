using System;
using _Project.Core.EventBus;
using _Project.Core.Factories;
using _Project.Core.Render.VFX;
using _Project.Core.Tools;
using _Project.Features.Common.EntitiesLifecycle.Events;

namespace _Project.Features.Common.EntitiesLifecycle
{
    public class EffectSpawner<TEvent, TSpawnData> : IDisposable
        where TEvent: ISpawnEvent<TSpawnData>
    {
        private readonly IFactory<TSpawnData, IEffect> _factory;
        // private readonly Storage<IEffect> _effectStorage;
        private readonly IEventBus _eventBus;


        public EffectSpawner(
            IFactory<TSpawnData, IEffect> factory,
            // Storage<IEffect> effectStorage,
            IEventBus  eventBus)
        {
            _factory = factory;
            // _effectStorage = effectStorage;
            _eventBus = eventBus;
            
            _eventBus.Subscribe<TEvent>(SpawnEffect);
        }

        private void SpawnEffect(TEvent @event)
        {
            var effect = _factory.Create(@event.SpawnData);
            // _effectStorage.Add(effect);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<TEvent>(SpawnEffect);
        }
    }
}