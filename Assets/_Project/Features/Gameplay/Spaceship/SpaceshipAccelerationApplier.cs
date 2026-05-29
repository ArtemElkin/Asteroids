using _Project.Core.Infrastructure.Config;
using _Project.Core.Math;
using Zenject;

namespace _Project.Features.Gameplay.Spaceship
{
    public class SpaceshipAccelerationApplier : IInitializable
    {
        private float _accelerationMultiplier;
        private readonly IConfigProvider _configProvider;


        public SpaceshipAccelerationApplier(
            IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public void Initialize()
        {
            var config = _configProvider.GetConfigFromJson<SpaceshipMovementConfig>("SpaceshipMovementConfig");
            _accelerationMultiplier = config.accelerationMultiplier;
        }

        public CustomVector2 ApplyAcceleration(CustomVector2 velocity, CustomVector2 direction, float deltaTime)
        {
            return velocity + _accelerationMultiplier * deltaTime * direction;
        }
    }
}