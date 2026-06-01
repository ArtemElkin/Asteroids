using _Project.Core.Math;

namespace _Project.Features.Spaceship.SpaceshipClone
{
    public struct SpaceshipCloneSpawnData
    {
        public Vector2 offsetFromMainSpaceship;


        public SpaceshipCloneSpawnData(Vector2 offsetFromMainSpaceship)
        {
            this.offsetFromMainSpaceship = offsetFromMainSpaceship;
        }
    }
}