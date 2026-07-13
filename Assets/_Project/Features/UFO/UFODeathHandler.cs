using _Project.Core.EventBus;
using _Project.Features.Common.EnemyAwardsService;
using _Project.Features.Common.EntitiesLifecycle.Events;
using _Project.Features.Common.Hit;
using _Project.Features.Common.Hit.Events;

namespace _Project.Features.UFO
{
    public class UFODeathHandler
    {
        private readonly IEventBus _eventBus;


        public UFODeathHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }


        public void HandleDeath(UFOFacade facade, HitInfo hitInfo, EnemyType enemyType)
        {
            _eventBus.Publish(new HitEvent(hitInfo));
            _eventBus.Publish(new DespawnRequestedEvent<UFOFacade>(facade));
            _eventBus.Publish(new EnemyDestroyedEvent(enemyType));
        }
    }
}