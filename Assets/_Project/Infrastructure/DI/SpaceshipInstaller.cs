using _Project.Core.Factories;
using _Project.Core.Tools;
using _Project.Features.Common.EntitiesLifecycle;
using _Project.Features.Common.ScreenWrapClone;
using _Project.Features.Spaceship;
using _Project.Infrastructure.Factories;
using _Project.Infrastructure.Render;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.DI
{
    public class SpaceshipInstaller : MonoInstaller
    {
        [SerializeField] private MovableView _spaceshipPrefab;
        [SerializeField] private TransformView _spaceshipScreenWrapClonePrefab;
        [SerializeField] private Transform _spaceshipParentTransform;
        
        
        public override void InstallBindings()
        {
            BindSpaceshipStorage();
            BindSpaceshipFactory(_spaceshipPrefab, _spaceshipParentTransform);
            BindSpaceshipSpawner();
            BindSpaceshipDespawner();
            
            BindSpaceshipCloneFactory(_spaceshipScreenWrapClonePrefab, _spaceshipParentTransform);
        }

        private void BindSpaceshipStorage()
        {
            Container
                .Bind<Storage<SpaceshipFacade>>()
                .AsSingle()
                .NonLazy();
        }

        private void BindSpaceshipFactory(
            MovableView spaceshipPrefab,
            Transform spaceshipParentTransform)
        {
            Container
                .Bind(
                    typeof(Core.Factories.IFactory<SpaceshipSpawnData, SpaceshipFacade>),
                    typeof(IReleaser<SpaceshipFacade>))
                .To<SpaceshipFactory>()
                .AsSingle()
                .WithArguments(spaceshipPrefab, spaceshipParentTransform)
                .NonLazy();
        }

        private void BindSpaceshipSpawner()
        {
            Container
                .BindInterfacesAndSelfTo<SpaceshipSpawner>()
                .AsSingle()
                .NonLazy();
        }

        private void BindSpaceshipDespawner()
        {
            Container
                .BindInterfacesAndSelfTo<Despawner<SpaceshipFacade>>()
                .AsSingle()
                .NonLazy();
        }

        private void BindSpaceshipCloneFactory(
            TransformView spaceshipPrefab,
            Transform spaceshipParentTransform)
        {
            Container
                .Bind<Core.Factories.IScreenWrapCloneFactory<ScreenWrapCloneSpawnData, SpaceshipFacade>>()
                .To<ScreenWrapCloneFactory<SpaceshipFacade>>()
                .AsSingle()
                .WithArguments(spaceshipPrefab, spaceshipParentTransform)
                .NonLazy();
        }
    }
}