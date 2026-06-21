using System;

namespace _Project.Features.Spaceship.Weapon.LaserWeapon
{
    public interface IReadOnlyLaserWeaponState
    {
        int AvailableBeamCount { get; }
        float RechargeTimeLeft { get; }
        event Action<int> AvailableBeamCountChanged;
        event Action<float> RechargeTimeLeftChanged;
    }
}