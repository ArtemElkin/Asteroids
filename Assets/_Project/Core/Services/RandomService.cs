using System;

namespace _Project.Core.Services
{
    public class RandomService : IRandomService
    {
        private const float DefaultAccuracy = 0.001f;
        private int _scale;
        private Random _random;


        public RandomService(int? seed = null, float accuracy = DefaultAccuracy)
        {
            _scale = (int)(1 /  accuracy);
            _random = seed.HasValue ? new Random(seed.Value) : new Random();
        }

        public int GetRandomNonNegativeInt(int max = Int32.MaxValue)
        {
            return _random.Next(max);
        }

        public float GetRandomFloat(float min = float.MinValue, float max = float.MaxValue)
        {
            var minScaled = (int)(min * _scale);
            var maxScaled = (int)(max * _scale);
            
            var newScaled =  _random.Next(minScaled, maxScaled);
            return newScaled / 1f / _scale;
        }
    }
}