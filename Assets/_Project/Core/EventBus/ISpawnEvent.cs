namespace _Project.Core.EventBus
{
    public interface ISpawnEvent<out TSpawnData> : IEvent
    {
        TSpawnData SpawnData { get; }
    }
}