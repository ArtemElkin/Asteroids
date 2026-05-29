using _Project.Core.Infrastructure.Config;
using _Project.Core.Math;
using Zenject;


namespace _Project.Features.Gameplay.Spaceship
{
    public class SpaceshipInertiaApplier : IInitializable
    {
        private float _inertiaMultiplier;
        private readonly IConfigProvider _configProvider;


        public SpaceshipInertiaApplier(
            IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public void Initialize()
        {
            var config = _configProvider.GetConfigFromJson<SpaceshipMovementConfig>("SpaceshipMovementConfig");
            _inertiaMultiplier = config.inertiaMultiplier;
        }

        public CustomVector2 ApplyInertia(CustomVector2 velocity, float deltaTime)
        {
            return velocity * (1 - _inertiaMultiplier * deltaTime);
        }
    }
}