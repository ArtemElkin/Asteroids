using _Project.Core.Save;
using _Project.Features.Common.Collision;

namespace _Project.Features.Common.Settings
{
    public sealed class SettingsModel : ISaveable<SettingsSave>
    {
        private SettingsSave _settingsSave = new();
        public CollisionResolverType CollisionResolverType => _settingsSave.collisionType;

        public void SetCollisionResolver(CollisionResolverType collisionType)
        {
            _settingsSave.collisionType = collisionType;
        }

        public SettingsSave GetSave() => _settingsSave.Clone();

        public void LoadSave(SettingsSave loadedSave)
        {
            if (loadedSave == null) return;
            _settingsSave = loadedSave.Clone();
        }
    }
}