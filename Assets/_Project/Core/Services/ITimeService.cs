using System;

namespace _Project.Core.Services
{
    public interface ITimeService
    {
        event Action<float> OnTick;
        event Action<float> OnFixedTick;
    }
}