using _Project.Core.Physics.Movement;
using _Project.Features.Spaceship.Config;

namespace _Project.Features.Spaceship
{
    public struct SpaceshipSpawnData
    {
        public readonly InitialMovementData initialMovementData;
        public readonly bool hasClones;
        public readonly SpaceshipConfig config;


        public SpaceshipSpawnData(InitialMovementData initialMovementData, bool hasClones, SpaceshipConfig config)
        {
            this.initialMovementData =  initialMovementData;
            this.hasClones = hasClones;
            this.config = config;
        }
    }
}