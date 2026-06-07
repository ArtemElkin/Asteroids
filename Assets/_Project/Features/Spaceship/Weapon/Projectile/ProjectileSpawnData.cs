using _Project.Core.Physics;

namespace _Project.Features.Spaceship.Weapon.Projectile
{
    public struct ProjectileSpawnData
    {
        public InitialMovementData initialMovementData;


        public ProjectileSpawnData(InitialMovementData initialMovementData)
        {
            this.initialMovementData = initialMovementData;
        }
    }
}