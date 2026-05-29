using _Project.Core.Infrastructure.Config;

namespace _Project.Features.Gameplay
{
    public class GameConfig : IConfig
    {
        public int maxAsteroidsCount;
        public int maxUFOsCount;
        public float spawnOffsetFromBounds;
    }
}