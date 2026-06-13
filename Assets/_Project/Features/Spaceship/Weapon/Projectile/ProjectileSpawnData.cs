using _Project.Core.Physics.Movement;

namespace _Project.Features.Spaceship.Weapon.Projectile
{
    public struct ProjectileSpawnData
    {
        public InitialMovementData initialMovementData;
        public float aliveTime;


        public ProjectileSpawnData(InitialMovementData initialMovementData, float aliveTime)
        {
            this.initialMovementData = initialMovementData;
            this.aliveTime = aliveTime;
        }
    }
}