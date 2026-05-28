using _Project.Core.Infrastructure.Config;

namespace _Project.Features.Gameplay
{
    public class GameConfig : IConfig
    {
        public int maxAsteroidsCount;
        public float spawnOffsetFromBounds;
    }
}