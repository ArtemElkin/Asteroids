using _Project.Core.Save;
using _Project.Features.Common.Collision;

namespace _Project.Features.Common.Settings
{
    public sealed class SettingsSave : ISave
    {
        public CollisionResolverType collisionType = CollisionResolverType.Elastic;
        
        public SettingsSave Clone()
        {
            return new SettingsSave
            {
                collisionType = this.collisionType,
            };
        }
    }
}