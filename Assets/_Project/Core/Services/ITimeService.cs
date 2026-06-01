using System;

namespace _Project.Core.Services
{
    public interface ITimeService
    {
        float DeltaTime { get; }
        float FixedDeltaTime { get; }
        event Action OnTick;
        event Action OnFixedTick;
    }
}