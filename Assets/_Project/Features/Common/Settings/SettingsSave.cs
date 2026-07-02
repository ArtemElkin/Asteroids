using _Project.Core.Save;
using _Project.Features.Common.Collision;

namespace _Project.Features.Common.Settings
{
    public sealed class SettingsSave : ISave
    {
        public const int MaxVolumeLevel = 10;

        public CollisionResolverType CollisionType { get; set; } = CollisionResolverType.Elastic;
        public bool SpaceshipClonesEnabled { get; set; } = true;
        public bool AsteroidsClonesEnabled { get; set; }
        
        public int SoundsVolume { get; set; } = MaxVolumeLevel;
        
        public int MusicVolume { get; set; } = MaxVolumeLevel;
        
        public SettingsSave Clone()
        {
            return new SettingsSave
            {
                CollisionType = this.CollisionType,
                SpaceshipClonesEnabled = this.SpaceshipClonesEnabled,
                AsteroidsClonesEnabled = this.AsteroidsClonesEnabled,
                SoundsVolume = this.SoundsVolume,
                MusicVolume = this.MusicVolume
            };
        }
    }
}