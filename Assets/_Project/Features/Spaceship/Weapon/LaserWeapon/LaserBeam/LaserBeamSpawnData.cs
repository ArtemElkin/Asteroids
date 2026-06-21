using _Project.Core.Math;

namespace _Project.Features.Spaceship.Weapon.LaserWeapon.LaserBeam
{
    public class LaserBeamSpawnData
    {
        public readonly Vector2 initialPosition;
        public readonly float initialRotationAngle;
        public readonly float aliveTime;


        public LaserBeamSpawnData(Vector2 initialPosition, float initialRotationAngle, float aliveTime)
        {
            this.initialPosition = initialPosition;
            this.initialRotationAngle = initialRotationAngle;
            this.aliveTime = aliveTime;
        }
    }
}