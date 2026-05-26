using _Project.Core.Infrastructure.Config;
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.Spaceship
{
    public class SpaceshipInertiaHandler : IInitializable
    {
        private float _inertiaMultiplier;
        private readonly IConfigProvider _configProvider;


        public SpaceshipInertiaHandler(
            IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public void Initialize()
        {
            var config = _configProvider.GetConfigFromJson<SpaceshipMovementConfig>("SpaceshipMovementConfig");
            _inertiaMultiplier = config.inertiaMultiplier;
        }

        public Vector2 ApplyInertia(Vector2 velocity, float deltaTime)
        {
            return velocity * (1 - _inertiaMultiplier * deltaTime);
        }
    }
}