using System;
using _Project.Core.Save;
using _Project.Features.Common.Collision;

namespace _Project.Features.Common.Settings
{
    public sealed class SettingsModel : ISaveable<SettingsSave>
    {
        public event Action<int> OnSoundsVolumeChanged;
        public event Action<int> OnMusicVolumeChanged;
        private SettingsSave _settingsSave = new();
        public CollisionResolverType CollisionResolverType => _settingsSave.CollisionType;
        public bool SpaceshipClonesEnabled => _settingsSave.SpaceshipClonesEnabled;
        public bool AsteroidsClonesEnabled => _settingsSave.AsteroidsClonesEnabled;
        public int SoundsVolume => _settingsSave.SoundsVolume;
        public int MusicVolume => _settingsSave.MusicVolume;

        public void SetCollisionResolver(CollisionResolverType collisionType)
        {
            _settingsSave.CollisionType = collisionType;
        }
        
        public void TurnSpaceshipClonesEnabled()
        {
            _settingsSave.SpaceshipClonesEnabled = !_settingsSave.SpaceshipClonesEnabled;
        }

        public void TurnAsteroidsClonesEnabled()
        {
            _settingsSave.AsteroidsClonesEnabled = !_settingsSave.AsteroidsClonesEnabled;
        }

        public void SetSoundsVolume(int value)
        {
            var clampedValue = Math.Clamp(value, 0, SettingsSave.MaxVolumeLevel);
            _settingsSave.SoundsVolume = clampedValue;
            OnSoundsVolumeChanged?.Invoke(clampedValue);
        }

        public void SetMusicVolume(int value)
        {
            var clampedValue = Math.Clamp(value, 0, SettingsSave.MaxVolumeLevel);
            _settingsSave.MusicVolume = clampedValue;
            OnMusicVolumeChanged?.Invoke(clampedValue);
        }

        public SettingsSave GetSave() => _settingsSave.Clone();

        public void LoadSave(SettingsSave loadedSave)
        {
            if (loadedSave == null) return;
            _settingsSave = loadedSave.Clone();
            OnSoundsVolumeChanged?.Invoke(loadedSave.SoundsVolume);
            OnMusicVolumeChanged?.Invoke(loadedSave.MusicVolume);
        }
    }
}