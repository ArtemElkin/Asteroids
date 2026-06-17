using _Project.Core.EventBus;

namespace _Project.Features.Common.EntitiesLifecycle.Events
{
    public interface ISpawnEvent<out TSpawnData> : IEvent
    {
        TSpawnData SpawnData { get; }
    }
}