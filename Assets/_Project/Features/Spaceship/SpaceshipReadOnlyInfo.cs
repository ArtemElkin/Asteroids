using _Project.Core.Physics;
using _Project.Features.Spaceship.Health;
using _Project.Features.Spaceship.Weapon.LaserWeapon;

namespace _Project.Features.Spaceship
{
    public class SpaceshipReadOnlyInfo
    {
        public IReadOnlyPosition Position { get; }
        public IReadOnlyRotation Rotation { get; }
        public IReadOnlyVelocity Velocity { get; }
        public IReadOnlyHealthModel HealthModel { get; }
        public IReadOnlyLaserWeaponState LaserWeaponState { get; }


        public SpaceshipReadOnlyInfo(
            IReadOnlyPosition position,
            IReadOnlyRotation rotation,
            IReadOnlyVelocity velocity,
            IReadOnlyHealthModel healthModel,
            IReadOnlyLaserWeaponState laserWeaponState)
        {
            Position = position;
            Rotation = rotation;
            Velocity = velocity;
            HealthModel = healthModel;
            LaserWeaponState = laserWeaponState;
        }
    }
}