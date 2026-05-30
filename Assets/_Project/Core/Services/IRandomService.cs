using System;

namespace _Project.Core.Services
{
    public interface IRandomService
    {
        int GetRandomNonNegativeInt(int max = Int32.MaxValue);
        float GetRandomFloat(float min = float.MinValue, float max = float.MaxValue);
    }
}