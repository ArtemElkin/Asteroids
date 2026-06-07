using System;
using System.Collections.Generic;
using _Project.Core.Math;
using _Project.Core.Physics;
using _Project.Core.Services;
using _Project.Core.Signals;
using _Project.Features.Common.Signals;

namespace _Project.Features.Common
{
    public class SimpleReflectionCollisionService : ICollisionService, IDisposable
    {
        private readonly HashSet<int> _hashes = new();
        private readonly ISignalBus _signalBus;
        private readonly ITimeService _timeService;
        

        public SimpleReflectionCollisionService(ISignalBus signalBus, ITimeService timeService)
        {
            _signalBus = signalBus;
            _timeService = timeService;
            _signalBus.Subscribe<CollisionDetectedSignal>(OnCollisionDetected);
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

        private void OnCollisionDetected(CollisionDetectedSignal signal)
        {
            ProcessCollision(signal.collisionData);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<CollisionDetectedSignal>(OnCollisionDetected);
            _timeService.OnFixedTick -= OnFixedTick;
        }
    }
}