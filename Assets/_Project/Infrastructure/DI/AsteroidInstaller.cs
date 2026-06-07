using _Project.Core.Tools;
using _Project.Features.Asteroid;
using _Project.Features.Common.Clone;
using _Project.Infrastructure.Factories;
using _Project.Infrastructure.UnityRender;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.DI
{
    public class AsteroidInstaller : MonoInstaller
    {
        [SerializeField] private Transform _asteroidsParentTransform;
        [SerializeField] MovableView _asteroidPrefab;
        
        
        public override void InstallBindings()
        {
            BindAsteroidsStorage();
            BindAsteroidFactory(_asteroidPrefab, _asteroidsParentTransform);
            BindAsteroidSpawner();
            BindAsteroidDespawner();

            BindAsteroidCloneStorage();
            BindAsteroidCloneFactory(_asteroidPrefab, _asteroidsParentTransform);
            BindAsteroidCloneSpawner();
            BindAsteroidCloneDespawner();
        }

        private void BindAsteroidsStorage()
        {
            Container
                .Bind<Storage<AsteroidFacade>>()
                .AsSingle();
        }

        private void BindAsteroidFactory(MovableView asteroidPrefab, Transform asteroidsParentTransform)
        {
            Container
                .Bind<Core.Factories.IFactory<AsteroidSpawnData, AsteroidFacade>>()
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
        
        private void BindAsteroidCloneStorage()
        {
            Container
                .Bind<CloneStorage<AsteroidFacade>>()
                .AsSingle()
                .NonLazy();
        }

        private void BindAsteroidCloneFactory(
            MovableView clonePrefab,
            Transform clonesParentTransform)
        {
            Container
                .Bind<Core.Factories.IFactory<CloneSpawnData, CloneFacade<AsteroidFacade>>>()
                .To<CloneFactory<AsteroidFacade>>()
                .AsSingle()
                .WithArguments(clonePrefab, clonesParentTransform)
                .NonLazy();
        }

        private void BindAsteroidCloneSpawner()
        {
            Container
                .BindInterfacesAndSelfTo<CloneSpawner<AsteroidFacade>>()
                .AsSingle()
                .NonLazy();
        }

        private void BindAsteroidCloneDespawner()
        {
            Container
                .BindInterfacesAndSelfTo<CloneDespawner<AsteroidFacade>>()
                .AsSingle()
                .NonLazy();
        }
    }
}