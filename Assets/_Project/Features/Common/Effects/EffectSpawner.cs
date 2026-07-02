using System;
using _Project.Core.EventBus;
using _Project.Core.Factories;
using _Project.Core.Physics;
using _Project.Core.Render.VFX;
using _Project.Core.Tools;

namespace _Project.Features.Common.Effects
{
    public class EffectSpawner<TEvent, TSpawnData> : IDisposable
        where TEvent: ISpawnEvent<TSpawnData> where TSpawnData : IHasPosition
    {
        private readonly IEffectFactory<TSpawnData> _factory;
        private readonly Storage<IEffect> _effectStorage;
        private readonly IEventBus _eventBus;


        public EffectSpawner(
            IEffectFactory<TSpawnData> factory,
            Storage<IEffect> effectStorage,
            IEventBus  eventBus)
        {
            _factory = factory;
            _effectStorage = effectStorage;
            _eventBus = eventBus;
            
            _eventBus.Subscribe<TEvent>(SpawnEffect);
        }

        private void SpawnEffect(TEvent @event)
        {
            var effect = _factory.Create(@event.SpawnData.Position);
            _effectStorage.Add(effect);
            void OnEndedHandler()
            {
                effect.OnEnded -= OnEndedHandler;
                _effectStorage.Remove(effect);
                _factory.Release(effect);
            }
            
            effect.OnEnded += OnEndedHandler;
            effect.Play();
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<TEvent>(SpawnEffect);
        }
    }
}