using _Project.Core.Physics;
using _Project.Features.Spaceship.Health;
using _Project.Features.Spaceship.Weapon.LaserWeapon;

namespace _Project.Features.Spaceship
{
    public class SpaceshipReadOnlyInfo
    {
        public IObservablePosition Position { get; }
        public IObservableRotation Rotation { get; }
        public IObservableVelocity Velocity { get; }
        public IReadOnlyHealthModel HealthModel { get; }
        public IReadOnlyLaserWeaponState LaserWeaponState { get; }


        public SpaceshipReadOnlyInfo(
            IObservablePosition position,
            IObservableRotation rotation,
            IObservableVelocity velocity,
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