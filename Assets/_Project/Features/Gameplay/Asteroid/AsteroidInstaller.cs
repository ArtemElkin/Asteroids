using _Project.Core.Tools;
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.Asteroid
{
    public class AsteroidInstaller : MonoInstaller
    {
        [SerializeField] private Transform _asteroidsParentTransform;
        [SerializeField] AsteroidView _asteroidPrefab;
        
        
        public override void InstallBindings()
        {
            BindAsteroidsStorage();
            BindAsteroidFactory(_asteroidPrefab, _asteroidsParentTransform);
            BindAsteroidSpawner();
            BindAsteroidDespawner();
        }

        private void BindAsteroidsStorage()
        {
            Container
                .Bind<Storage<AsteroidFacade>>()
                .AsSingle();
        }

        private void BindAsteroidFactory(AsteroidView asteroidPrefab, Transform asteroidsParentTransform)
        {
            Container
                .Bind<Infrastructure.Factories.IFactory<AsteroidSpawnData, AsteroidFacade>>()
                .To<AsteroidFactory>()
                .AsSingle()
                .WithArguments(asteroidPrefab, asteroidsParentTransform)
                .NonLazy();
        }

        private void BindAsteroidSpawner()
        {
            Container
                .BindInterfacesAndSelfTo<AsteroidSpawner>()
                .AsSingle();
        }

        private void BindAsteroidDespawner()
        {
            Container
                .BindInterfacesAndSelfTo<AsteroidDespawner>()
                .AsSingle()
                .NonLazy();
        }
    }
}