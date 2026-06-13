using _Project.Core.Config;

namespace _Project.Features.Common.Config
{
    public class GameConfig : IConfig
    {
        public int maxAsteroidsCount;
        public int maxUFOsCount;
        public float spawnOffsetFromBounds;
        public float spawnInterval;
    }
}