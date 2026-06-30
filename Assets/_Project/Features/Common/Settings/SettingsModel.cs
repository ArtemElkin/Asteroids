using _Project.Core.Save;
using _Project.Features.Common.Collision;

namespace _Project.Features.Common.Settings
{
    public sealed class SettingsModel : ISaveable<SettingsSave>
    {
        private SettingsSave _settingsSave = new();
        public CollisionResolverType CollisionResolverType => _settingsSave.CollisionType;
        public bool SpaceshipClonesEnabled => _settingsSave.SpaceshipClonesEnabled;
        public bool AsteroidsClonesEnabled => _settingsSave.AsteroidsClonesEnabled;

        public void SetCollisionResolver(CollisionResolverType collisionType)
        {
            _settingsSave.CollisionType = collisionType;
        }
        
        public void TurnSpaceshipClonesEnabled()
        {
            _settingsSave.SpaceshipClonesEnabled = !_settingsSave.SpaceshipClonesEnabled;;
        }

        public void TurnAsteroidsClonesEnabled()
        {
            _settingsSave.AsteroidsClonesEnabled = !_settingsSave.AsteroidsClonesEnabled;;
        }

        public SettingsSave GetSave() => _settingsSave.Clone();

        public void LoadSave(SettingsSave loadedSave)
        {
            if (loadedSave == null) return;
            _settingsSave = loadedSave.Clone();
        }
    }
}