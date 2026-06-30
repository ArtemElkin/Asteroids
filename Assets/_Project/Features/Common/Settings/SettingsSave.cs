using _Project.Core.Save;
using _Project.Features.Common.Collision;

namespace _Project.Features.Common.Settings
{
    public sealed class SettingsSave : ISave
    {
        public CollisionResolverType CollisionType { get; set; } = CollisionResolverType.Elastic;
        public bool SpaceshipClonesEnabled { get; set; } = true;
        public bool AsteroidsClonesEnabled { get; set; }
        
        public SettingsSave Clone()
        {
            return new SettingsSave
            {
                CollisionType = this.CollisionType,
                SpaceshipClonesEnabled = this.SpaceshipClonesEnabled,
                AsteroidsClonesEnabled = this.AsteroidsClonesEnabled
            };
        }
    }
}