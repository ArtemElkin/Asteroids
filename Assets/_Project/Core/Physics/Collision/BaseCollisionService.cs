using System;
using System.Collections.Generic;
using _Project.Core.EventBus;
using _Project.Core.Physics.Collision.Events;
using _Project.Core.Services;

namespace _Project.Core.Physics.Collision
{
    public abstract class BaseCollisionService : IDisposable
    {
        private readonly HashSet<int> _hashes = new();
        private readonly IEventBus _eventBus;
        private readonly ITimeService _timeService;
        

        protected BaseCollisionService(IEventBus eventBus, ITimeService timeService)
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

        private void ProcessCollision(CollisionData collisionData)
        {
            var hashA = collisionData.modelA.GetHashCode();
            var hashB = collisionData.modelB.GetHashCode();
            var minHash = hashA < hashB ? hashA : hashB;
            var maxHash = hashA > hashB ? hashA : hashB;
            var hash = HashCode.Combine(minHash, maxHash);
            
            if (_hashes.Contains(hash)) return;
            
            OnProcessCollision(collisionData);
            
            _hashes.Add(hash);
        }

        protected virtual void OnProcessCollision(CollisionData collisionData) { }

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