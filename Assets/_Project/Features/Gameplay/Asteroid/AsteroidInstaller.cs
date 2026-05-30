using _Project.Core.Tools;
using _Project.Features.Gameplay.Common;
using _Project.Features.Gameplay.Signals;
using _Project.Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace _Project.Features.Gameplay.Asteroid
{
    public class AsteroidInstaller : MonoInstaller
    {
        [SerializeField] private Transform _asteroidsParentTransform;
        [SerializeField] AsteroidComponent _asteroidPrefab;
        
        
        public override void InstallBindings()
        {
            BindAsteroidMovementController();
            BindAsteroidsStorage();
            BindAsteroidFactory(_asteroidPrefab, _asteroidsParentTransform);
            BindAsteroidSpawner();
            BindAsteroidSpawnTimer();
            BindAsteroidDespawner();
        }

        private void BindAsteroidMovementController()
        {
            Container
                .Bind<AsteroidMovementController>()
                .AsTransient();
        }

        private void BindAsteroidsStorage()
        {
            Container
                .Bind<Storage<AsteroidComponent>>()
                .AsSingle();
        }

        private void BindAsteroidFactory(AsteroidComponent asteroidPrefab, Transform asteroidsParentTransform)
        {
            Container
                .Bind<FactoryWithPool<AsteroidComponent>>()
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

        private void BindAsteroidSpawnTimer()
        {
            Container.DeclareSignal<SpawnRequestedSignal<AsteroidComponent>>();
            
            Container
                .BindInterfacesAndSelfTo<SpawnTimer<AsteroidComponent>>()
                .AsSingle()
                .NonLazy();
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