using _Project.Core.EventBus;
using _Project.Features.Common.EnemyAwardsService;
using _Project.Features.Common.EntitiesLifecycle.Events;
using _Project.Features.Common.Hit;
using _Project.Features.Common.Hit.Events;

namespace _Project.Features.Asteroid
{
    public class AsteroidDeathHandler
    {
        private readonly AsteroidDestructor _destructor;
        private readonly IEventBus _eventBus;


        public AsteroidDeathHandler(AsteroidDestructor destructor, IEventBus eventBus)
        {
            _destructor = destructor;
            _eventBus = eventBus;
        }


        public void HandleDeath(AsteroidFacade facade, HitInfo hitInfo, EnemyType enemyType)
        {
            _eventBus.Publish(new HitEvent(hitInfo));
            _destructor.Destruct(facade, hitInfo.fullDestroy);
            _eventBus.Publish(new EnemyDestroyedEvent(enemyType));
        }
    }
}