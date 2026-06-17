using _Project.Features.Common.EntitiesLifecycle.Events;

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