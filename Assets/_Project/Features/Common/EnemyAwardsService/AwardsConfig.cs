using System.Collections.Generic;
using _Project.Core.Config;

namespace _Project.Features.Common.EnemyAwardsService
{
    public class AwardsConfig : IConfig
    {
        public Dictionary<EnemyType, int> EnemyAwards { get; set; }
    }
}