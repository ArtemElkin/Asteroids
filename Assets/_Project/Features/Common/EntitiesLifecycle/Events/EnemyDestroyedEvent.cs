using _Project.Core.EventBus;
using _Project.Features.Common.EnemyAwardsService;

namespace _Project.Features.Common.EntitiesLifecycle.Events
{
    public class EnemyDestroyedEvent : IEvent
    {
        public EnemyType Type { get; set; }


        public EnemyDestroyedEvent(EnemyType type)
        {
            Type = type;
        }
    }
}