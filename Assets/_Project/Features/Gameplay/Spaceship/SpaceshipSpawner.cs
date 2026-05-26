using Zenject;

namespace _Project.Features.Gameplay.Spaceship
{
    public class SpaceshipSpawner : IInitializable
    {
        private readonly SpaceshipComponent _spaceshipPrefab;
        private readonly IInstantiator _instantiator;


        public SpaceshipSpawner(
            SpaceshipComponent spaceshipPrefab,
            IInstantiator instantiator)
        {
            _spaceshipPrefab = spaceshipPrefab;
            _instantiator = instantiator;
        }

        public void Initialize()
        {
            SpawnSpaceship();
        }

        private void SpawnSpaceship()
        {
            _instantiator.InstantiatePrefabForComponent<SpaceshipComponent>(_spaceshipPrefab);
        }

    }
}