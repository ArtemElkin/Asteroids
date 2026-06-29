using System;
using System.Collections.Generic;
using _Project.Core.EventBus;
using _Project.Core.Physics.Collision;
using _Project.Core.Physics.Collision.Events;
using _Project.Core.Save;
using _Project.Core.Services;
using _Project.Core.StaticData;
using _Project.Features.Common.Settings;

namespace _Project.Features.Common.Collision
{
    public class CollisionService : IDisposable
    {
        private readonly HashSet<int> _hashes = new();
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
            _hashes.Clear();
        }

        private void ProcessCollision(CollisionData collisionData)
        {
            var hashA = collisionData.modelA.GetHashCode();
            var hashB = collisionData.modelB.GetHashCode();
            var minHash = hashA < hashB ? hashA : hashB;
            var maxHash = hashA > hashB ? hashA : hashB;
            var hash = HashCode.Combine(minHash, maxHash);
            
            if (_hashes.Contains(hash)) return;
            
            _collisionResolvers[_settingsModel.CollisionResolverType].ProcessCollision(collisionData);
            
            _eventBus.Publish(new CollisionProcessedEvent(collisionData));
            _hashes.Add(hash);
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