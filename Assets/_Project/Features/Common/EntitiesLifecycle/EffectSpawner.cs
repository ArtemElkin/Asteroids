using System;
using _Project.Core.EventBus;
using _Project.Core.Factories;
using _Project.Core.Physics;
using _Project.Core.Render.VFX;

namespace _Project.Features.Common.EntitiesLifecycle
{
    public class EffectSpawner<TEvent, TSpawnData> : IDisposable
        where TEvent: ISpawnEvent<TSpawnData> where TSpawnData : IHasPosition
    {
        private readonly IEffectFactory<TSpawnData> _factory;
        // private readonly Storage<IEffect> _effectStorage;
        private readonly IEventBus _eventBus;


        public EffectSpawner(
            IEffectFactory<TSpawnData> factory,
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
            var effect = _factory.Create(@event.SpawnData.Position);
            effect.Play();
            // _effectStorage.Add(effect);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<TEvent>(SpawnEffect);
        }
    }
}