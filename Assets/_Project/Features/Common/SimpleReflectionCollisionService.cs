using System;
using System.Collections.Generic;
using _Project.Core.EventBus;
using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Features.Common.Event;

namespace _Project.Features.Common
{
    public class SimpleReflectionCollisionService : ICollisionService, IDisposable
    {
        private readonly HashSet<int> _hashes = new();
        private readonly IEventBus _eventBus;
        private readonly ITimeService _timeService;
        

        public SimpleReflectionCollisionService(IEventBus eventBus, ITimeService timeService)
        {
            _eventBus = eventBus;
            _timeService = timeService;
            _eventBus.Subscribe<CollisionDetectedEvent>(OnCollisionDetected);
            _timeService.OnFixedTick += OnFixedTick;
        }

        private void OnFixedTick(float fixedDeltaTime)
        {
            _hashes.Clear();
        }

        public void ProcessCollision(CollisionData collisionData)
        {
            var hashA = collisionData.modelA.GetHashCode();
            var hashB = collisionData.modelB.GetHashCode();
            var minHash = hashA < hashB ? hashA : hashB;
            var maxHash = hashA > hashB ? hashA : hashB;
            var hash = HashCode.Combine(minHash, maxHash);
            
            if (_hashes.Contains(hash)) return;
            
            var velocityA = collisionData.modelA.Velocity;
            var velocityB = collisionData.modelB.Velocity;

            var velocityANew = Vector2.Reflect(velocityA, collisionData.collisionNormal);
            var velocityBNew = Vector2.Reflect(velocityB, collisionData.collisionNormal);
            
            collisionData.modelA.UpdateVelocity(velocityANew);
            collisionData.modelB.UpdateVelocity(velocityBNew);
            
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