using _Project.Core.Physics;

namespace _Project.Core.EventBus
{
    public interface ISpawnEvent<out TSpawnData> : IEvent where TSpawnData : IHasPosition
    {
        TSpawnData SpawnData { get; }
    }
}