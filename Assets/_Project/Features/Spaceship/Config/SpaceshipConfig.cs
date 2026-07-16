using _Project.Core.Config;
using _Project.Features.Spaceship.Health.Config;
using _Project.Features.Spaceship.Weapon.LaserWeapon.Config;
using _Project.Features.Spaceship.Weapon.ProjectileWeapon.Config;

namespace _Project.Features.Spaceship.Config
{
    public class SpaceshipConfig : IConfig
    {
        public float stunDuration;
        public HealthConfig healthConfig;
        public SpaceshipMovementConfig movementConfig;
        public ProjectileWeaponConfig projectileWeaponConfig;
        public LaserWeaponConfig laserWeaponConfig;
    }
}