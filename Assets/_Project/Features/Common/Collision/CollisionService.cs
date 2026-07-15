using System;
using System.Collections.Generic;
using _Project.Core.EventBus;
using _Project.Core.Physics.Collision;
using _Project.Core.Physics.Collision.Events;
using _Project.Core.Services;
using _Project.Features.Common.Settings;
using UnityEngine;

namespace _Project.Features.Common.Collision
{
    public class CollisionService : IDisposable
    {
        private readonly HashSet<CollisionPair> _processedPairs = new();
        private readonly Dictionary<CollisionResolverType, ICollisionResolver> _collisionResolvers = new();
        private readonly SettingsModel _settingsModel;
        private readonly IEventBus _eventBus;
        private readonly ITimeService _timeService;
        

        public CollisionService(
            List<ICollisionResolver> collisionResolvers,
            SettingsModel settingsModel,
            IEventBus eventBus, 
            ITimeService timeService)
        {
            foreach (var resolver in collisionResolvers) _collisionResolvers.Add(resolver.ResolverType, resolver);
            _settingsModel = settingsModel;
            _eventBus = eventBus;
            _timeService = timeService;
            _eventBus.Subscribe<CollisionDetectedEvent>(OnCollisionDetected);
            _timeService.OnFixedTick += OnFixedTick;
        }

        private void OnFixedTick(float fixedDeltaTime)
        {
            _processedPairs.Clear();
        }

        private void ProcessCollision(CollisionData collisionData)
        {
            var pair = new CollisionPair(collisionData.modelA, collisionData.modelB);
            
            if (!_processedPairs.Add(pair))
                return;

            if (_collisionResolvers.TryGetValue(_settingsModel.CollisionResolverType, out var resolver))
            {
                resolver.ProcessCollision(collisionData);
                _eventBus.Publish(new CollisionProcessedEvent(collisionData));
            }
            else
            {
                Debug.LogError("Collision Resolver not found in dictionary");
            }
        }

        private void OnCollisionDetected(CollisionDetectedEvent @event)
        {
            ProcessCollision(@event.collisionData);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<CollisionDetectedEvent>(OnCollisionDetected);
            _timeService.OnFixedTick -= OnFixedTick;
        }
    }
}