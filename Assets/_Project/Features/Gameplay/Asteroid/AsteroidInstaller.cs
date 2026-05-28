using _Project.Core.Tools;
using _Project.Features.Gameplay.Signals;
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
            BindAsteroidFactory(_asteroidPrefab, _asteroidsParentTransform);
            BindAsteroidSpawner();
            BindAsteroidSpawnTimer();
            BindAsteroidDespawner();
        }

        private void BindAsteroidMovementController()
        {
            Container
                .BindInterfacesAndSelfTo<AsteroidMovementController>()
                .AsTransient();
        }

        private void BindAsteroidFactory(AsteroidComponent asteroidPrefab, Transform asteroidsParentTransform)
        {
            Container
                .BindInterfacesAndSelfTo<FactoryWithPool<AsteroidComponent>>()
                .AsSingle()
                .WithArguments(asteroidPrefab, asteroidsParentTransform);
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
                .AsSingle();
        }

        private void BindAsteroidDespawner()
        {
            Container
                .BindInterfacesAndSelfTo<AsteroidDespawner>()
                .AsSingle();
        }
    }
}