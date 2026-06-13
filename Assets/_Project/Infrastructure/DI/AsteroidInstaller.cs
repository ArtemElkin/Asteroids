using _Project.Core.Factories;
using _Project.Core.Tools;
using _Project.Features.Asteroid;
using _Project.Features.Common;
using _Project.Features.Common.EntitiesLifecycle;
using _Project.Features.Common.ScreenWrapClone;
using _Project.Infrastructure.Factories;
using _Project.Infrastructure.Render;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.DI
{
    public class AsteroidInstaller : MonoInstaller
    {
        [SerializeField] MovableView _asteroidPrefab;
        [SerializeField] TransformView _asteroidScreenWrapClonePrefab;
        [SerializeField] private Transform _asteroidsParentTransform;
        
        
        public override void InstallBindings()
        {
            BindAsteroidsStorage();
            BindAsteroidFactory(_asteroidPrefab, _asteroidsParentTransform);
            BindAsteroidSpawner();
            BindAsteroidDespawner();

            BindAsteroidCloneFactory(_asteroidScreenWrapClonePrefab, _asteroidsParentTransform);
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
                .Bind(
                    typeof(Core.Factories.IFactory<AsteroidSpawnData, AsteroidFacade>),
                    typeof(IReleaser<AsteroidFacade>))
                .To<AsteroidFactory>()
                .AsSingle()
                .WithArguments(asteroidPrefab, asteroidsParentTransform)
                .NonLazy();
        }

        private void BindAsteroidSpawner()
        {
            Container
                .BindInterfacesAndSelfTo<AsteroidSpawner>()
                .AsSingle()
                .NonLazy();
        }

        private void BindAsteroidDespawner()
        {
            Container
                .BindInterfacesAndSelfTo<Despawner<AsteroidFacade>>()
                .AsSingle()
                .NonLazy();
        }

        private void BindAsteroidCloneFactory(
            TransformView clonePrefab,
            Transform clonesParentTransform)
        {
            Container
                .Bind<IScreenWrapCloneFactory<ScreenWrapCloneSpawnData, AsteroidFacade>>()
                .To<ScreenWrapCloneFactory<AsteroidFacade>>()
                .AsSingle()
                .WithArguments(clonePrefab, clonesParentTransform)
                .NonLazy();
        }
    }
}