using _Project.Core.EventBus;

namespace _Project.Features.Common.Hit.Events
{
    public class HitEvent : ISpawnEvent<HitInfo>
    {
        public HitInfo SpawnData { get; }


        public HitEvent(HitInfo spawnData)
        {
            SpawnData = spawnData;
        }
    }
}